using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// 巣統合システム。
    /// シングルトンで動くので、NestSystem.Instanceでシングルトンインスタンスにアクセス可能。
    /// </summary>
    public class NestSystem : MonoBehaviour, IDebugMenu
    {
        public NestData Data => GameContext.Current?.s_NestData;

        #region Singleton Implementation

        public static NestSystem Instance { get; private set; }

        /// <summary>
        /// 自身をSingletonのインスタンスとして登録。既に別のインスタンスが存在する場合はfalseを返す。
        /// </summary>
        /// <returns></returns>
        private bool RegisterSingletonInstance()
        {
            if (!Instance)
            {
                Instance = this;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        private void Awake()
        {
            if (!RegisterSingletonInstance())
            {
                Destroy(gameObject);
                return;
            }
        }

        [SerializeField] private AntFactory[] antFactories = default;
        [SerializeField] private NestElementFactory[] nestElementFactories = default;
        
        public BuildingSystem BuildingSystem { get; set; }
        
        private readonly List<Ant> spawnedAnts = new List<Ant>();
        public IReadOnlyList<Ant> SpawnedAnt => spawnedAnts;
        
        private readonly List<NestElement> nestElements = new List<NestElement>();
        public IReadOnlyList<NestElement> NestElements => nestElements;
        
        private readonly List<NestPathElementEdge> elementEdges = new List<NestPathElementEdge>();
        public IReadOnlyList<NestPathElementEdge> ElementEdges => elementEdges;

        private void Start()
        {
            if (SaveUnit.Current != null)
            {
                //すでにロード済み
                LoadData();
            }

            //次にセーブデータが変更（ロード）されたときに、巣を更新する
            SaveUnit.OnCurrentSaveUnitChanged.Subscribe(su => LoadData());
            
            BuildingSystem = new BuildingSystem(this);
        }

        /// <summary>
        /// NestDataをもとに巣を再構築する。
        /// </summary>
        private void LoadData()
        {
            //生成済みのアリを破棄
            foreach (var ant in spawnedAnts)
            {
                Destroy(ant.gameObject);
            }

            spawnedAnts.Clear();

            //生成済みの部屋や道を破棄
            foreach (var element in nestElements)
            {
                Destroy(element.gameObject);
            }

            nestElements.Clear();

            elementEdges.Clear();


            //セーブデータから巣情報をロード

            //アリを生成
            foreach (var antData in Data.Ants)
            {
                var ant = InstantiateAnt(antData, false);
            }

            //道、部屋を生成
            foreach (var elementData in Data.Structure.NestElements)
            {
                var element = InstantiateNestElement(elementData, false);
            }

            //Element間の接続をロード
            foreach (var edgeData in Data.Structure.ElementEdges)
            {
                var edge = new NestPathElementEdge(nestElements, edgeData);
                elementEdges.Add(edge);
            }
        }


        /// <summary>
        /// AntDataをもとに、対応するAntFactoryを用いてAntのインスタンスを生成する。
        /// </summary>
        /// <param name="antData">生成に使用するAntData。</param>
        /// <param name="registerToGameContext">新たにGameContextに登録するかどうか。セーブデータからの生成などの際に限りfalseを指定する。</param>
        /// <returns>生成されたGameObjectのもつAntコンポーネント。</returns>
        public Ant InstantiateAnt(AntData antData, bool registerToGameContext = true)
        {
            //Debug.Log(antData.GetType());
            var ant = antFactories.FirstOrDefault(f => f.DataType == antData.GetType())?.InstantiateAnt(antData);
            if (ant != null)
            {
                if (registerToGameContext)
                {
                    Data.Ants.Add(antData);
                }

                spawnedAnts.Add(ant);
            }

            return ant;
        }

        /// <summary>
        /// InstantiateAntで生成したアリを破棄します。
        /// セーブデータから該当のアリを削除します。
        /// GameObjectの破棄までは担当しないので、呼び出し側でDestroyしてください。
        /// </summary>
        /// <param name="ant"></param>
        public void RemoveAnt(Ant ant)
        {
            if (Data.Ants.Contains(ant.Data))
            {
                Data.Ants.Remove(ant.Data);
            }
        }


        /// <summary>
        /// NestElementDataをもとに、対応するNestElementFactoryを用いてNestElementのインスタンスを生成する。
        /// </summary>
        /// <param name="elementData">生成に使用するNestElementData。</param>
        /// <param name="registerToGameContext">新たにGameContextに登録するかどうか。セーブデータからの生成などの際に限りfalseを指定する。</param>
        /// <returns>生成されたGameObjectのもつNestElementコンポーネント。</returns>
        public NestElement InstantiateNestElement(NestElementData elementData, bool registerToGameContext = true)
        {
            //Debug.Log(elementData.GetType());
            var nestElement = nestElementFactories.FirstOrDefault(f => f.DataType == elementData.GetType())
                ?.InstantiateNestElement(elementData);
            if (nestElement != null)
            {
                if (registerToGameContext)
                {
                    Data.Structure.NestElements.Add(elementData);
                }

                nestElements.Add(nestElement);
            }

            return nestElement;
        }

        /// <summary>
        /// InstantiateNestElement()で追加したNestElementを削除します。
        /// セーブデータからNestElementを削除し、関連するElement間接続を破棄します。
        /// GameObjectの破棄までは担当しないので、呼び出し側でDestroyしてください
        /// </summary>
        /// <param name="element"></param>
        public void RemoveNestElement(NestElement element)
        {
            if (Data.Structure.NestElements.Contains(element.Data))
            {
                Data.Structure.NestElements.Remove(element.Data);
                //Element間接続がある場合はその接続も破棄
                var l = elementEdges.Where(e => e.A.Host == element || e.B.Host == element).ToList();
                foreach (var e in l)
                {
                    if (Data.Structure.ElementEdges.Contains(e.Data))
                    {
                        Data.Structure.ElementEdges.Remove(e.Data);
                    }
                    elementEdges.Remove(e);
                }
                
            }

        }

        /// <summary>
        /// 外部で生成したNestElementをNestSystemに登録する。
        /// あらかじめNestElement.Initializeで適切なNestElementDataが注入されている必要がある。
        /// </summary>
        /// <param name="element"></param>
        public void AddNestElement(NestElement element)
        {
            if(nestElements.Contains(element)) throw new ArgumentException("指定されたNestElementはすでに登録されています。");
            if(element.Data == null) throw new ArgumentException("NestElementDataが設定されていません。あらかじめ適切なNestElementDataを注入してください。");
            if(Data.Structure.NestElements.Contains(element.Data)) throw new ArgumentException("指定されたNestElementのDataはすでに登録されています。");
            nestElements.Add(element);
            Data.Structure.NestElements.Add(element.Data);
        }

        public NestPathElementEdge ConnectElements(NestPathNode a, NestPathNode b)
        {
            var edge = new NestPathElementEdge(a, b);

            elementEdges.Add(edge);
            Data.Structure.ElementEdges.Add(edge.Data);
            return edge;
        }

        #region Debug

        public string pageTitle { get; } = "巣統合システム";
        private bool showGraph = false;

        public void OnGUIPage()
        {
            GUILayout.Label($"データのロード: {(Data != null ? "済" : "未")}");
            if (Data != null)
            {
                GUILayout.Label($"SpawnedAnts: {spawnedAnts.Count}");
                GUILayout.Label($"NestElements: {nestElements.Count}");
                if (GUILayout.Button("デバッグアリのスポーン"))
                {
                    InstantiateAnt(new DebugAntData());
                }

                if (GUILayout.Button("デバッグ部屋のスポーン"))
                {
                    InstantiateNestElement(new NestDebugRoomData()
                        {Position = new Vector2(nestElements.Count * 4 - 10, 0)});
                }

                if (GUILayout.Button("NestElement間を接続"))
                {
                    var nodeA = nestElements[0].GetNodes().First(n => n.IsExposed);
                    var nodeB = nestElements[1].GetNodes().Last(n => n.IsExposed);
                    ConnectElements(nodeA, nodeB);
                }

                showGraph = GUILayout.Toggle(showGraph, "経路グラフを表示");
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var e in nestElements)
            {
                Gizmos.color = Color.green;
                foreach (var edge in e.GetLocalEdges())
                {
                    var a = edge.A.WorldPosition;
                    var b = edge.B.WorldPosition;
                    Gizmos.DrawLine(a, b);
                }
            }

            Gizmos.color = Color.red;
            foreach (var edge in elementEdges)
            {
                var a = edge.A.WorldPosition;
                var b = edge.B.WorldPosition;
                Gizmos.DrawLine(a, b);
            }
        }

        #endregion
    }
}