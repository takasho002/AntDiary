using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace AntDiary
{
    public class GeneSystem : MonoBehaviour
    {
        public GeneData Data => GameContext.Current.s_GeneData;

        #region Singleton Implementation

        public static GeneSystem Instance { get; private set; }

        /// <summary>
        /// 自身をSingletonのインスタンスとして登録。既に別のインスタンスが存在する場合はfalseを返す。
        /// </summary>
        /// <returns></returns>
        private bool RegisterSingletonInstance()
        {
            if (Instance == null)
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

        [SerializeField] private GeneTreeEntry[] treeEntries = default;

        private bool isTreesInitialized = false;

        public IObservable<Gene> OnGeneActivated => onGeneActivated;
        private Subject<Gene> onGeneActivated = new Subject<Gene>();

        /// <summary>
        /// GeneSystemに登録されている遺伝子ツリーのリスト
        /// </summary>
        public IEnumerable<GeneTree> Trees
        {
            get
            {
                InitializeGeneTrees();
                return treeEntries.Select(e => e.GeneTreeAsset);
            }
        }

        public void Awake()
        {
            if (!RegisterSingletonInstance())
            {
                throw new InvalidOperationException("Duplicate GeneSystem singleton instance");
            }
        }

        private void Start()
        {
            InitializeGeneTrees();
        }

        /// <summary>
        /// Gene間の接続情報を適用します。
        /// </summary>
        private void InitializeGeneTrees()
        {
            if (isTreesInitialized) return;
            isTreesInitialized = true;
            foreach (var geneTree in Trees)
            {
                geneTree.ApplyEdges();
            }
        }

        /// <summary>
        /// 指定した遺伝子を開放する。
        /// <param name="tree">解放するGeneを含むGeneTree</param>
        /// <param name="gene">解放するGene</param>
        /// <returns>解放に成功したかどうか。</returns>
        /// </summary>
        public bool Activate(GeneTree tree, Gene gene)
        {
            if (!Trees.Contains(tree)) throw new ArgumentException("指定したGeneTreeはGeneSystemに登録されていません。");
            if (!tree.Genes.Contains(gene)) throw new ArgumentException("指定したGeneはGeneTreeに含まれていません。");

            if (gene.ParentGene != null && !Data.ActivatedGenes.Contains(gene.ParentGene.Guid))
            {
                //親ノードの遺伝子が解放されていなければ、解放できない
                return false;
            }

            var entry = treeEntries.FirstOrDefault(e => e.GeneTreeAsset == tree);
            if (entry != null && entry.ReleaseGeneDefinition != null)
            {
                if (!entry.ReleaseGeneDefinition.Release(gene.Id))
                {
                    return false;
                }
            }
            else
            {
                Debug.LogWarning("GeneSystem: 指定されたGeneに対応するGeneTreeActionが登録されていません。");
            }

            Data.ActivatedGenes.Add(gene.Guid);
            onGeneActivated.OnNext(gene);
            return true;

            //TODO: スキルポイント的なやつの消費
        }
    }

    /// <summary>
    /// 遺伝子システムに登録する遺伝子ツリーなどのエントリ。
    /// </summary>
    [Serializable]
    public class GeneTreeEntry
    {
        /// <summary>
        /// 遺伝子ツリーのデータを格納したアセット。
        /// </summary>
        public GeneTree GeneTreeAsset => geneTreeAsset;

        [SerializeField] private GeneTree geneTreeAsset = default;

        /// <summary>
        /// 遺伝子が解放されたときの動作を定義するGeneTreeActionの参照。
        /// </summary>
        public GeneTreeAction ReleaseGeneDefinition => releaseGeneDefinition;

        [SerializeField] private GeneTreeAction releaseGeneDefinition = default;
    }
}