using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// UIGeneTreeViewに表示される遺伝子ノードを示します。
    /// </summary>
    public class UIGeneTreeNode : MonoBehaviour
    {
        public void Initialize(Gene targetGene, int depth, int order)
        {
            TargetGene = targetGene;
            Depth = depth;
            Order = order;
        }

        public Gene TargetGene { get; private set; }
        public int Depth { get; set; }
        public int Order { get; set; }

        private RectTransform rectTransform;

        public RectTransform RectTransform
        {
            get
            {
                if (!rectTransform) rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }

        public Vector2 Position
        {
            get => RectTransform.anchoredPosition;
            set => RectTransform.anchoredPosition = value;
        }
        
        
        public List<UIGeneTreeNode> ChildGeneNodes { get; } = new List<UIGeneTreeNode>();
        
    }
}