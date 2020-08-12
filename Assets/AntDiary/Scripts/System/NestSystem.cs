using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
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
        private List<Ant> SpawnedAnts = new List<Ant>();

        private void Start()
        {
            if (SaveUnit.Current != null)
            {
                //すでにロード済み
                LoadData();
            }
            //次にセーブデータが変更（ロード）されたときに、巣を更新する
            SaveUnit.OnCurrentSaveUnitChanged.Subscribe(su => LoadData());
        }

        /// <summary>
        /// NestDataをもとに巣を再構築する。
        /// </summary>
        private void LoadData()
        {
            //生成済みのアリを破棄
            foreach (var ant in SpawnedAnts)
            {
                Destroy(ant.gameObject);
            }
            SpawnedAnts.Clear();
            
            //セーブデータから巣情報をロード
            foreach (var antData in Data.Ants)
            {
                var ant = InstantiateAnt(antData, false);
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
            Debug.Log(antData.GetType());
            var ant = antFactories.FirstOrDefault(f => f.DataType == antData.GetType())?.InstantiateAnt(antData);
            if (ant != null)
            {
                if (registerToGameContext)
                {
                    Data.Ants.Add(antData);
                }

                SpawnedAnts.Add(ant);
            }

            return ant;
        }

        #region Debug
        public string pageTitle { get; } = "巣統合システム";
        public void OnGUIPage()
        {
            GUILayout.Label($"データのロード: {(Data != null ? "済" : "未")}");
            if (Data != null)
            {
                GUILayout.Label($"SpawnedAnts: {SpawnedAnts.Count}");
                if (GUILayout.Button("デバッグアリのスポーン"))
                {
                    InstantiateAnt(new DebugAntData());
                }
            }
        }
        #endregion
    }
}