using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    public class GeneralPathRoad : NestElement<GeneralPathRoadData>
    {
        [SerializeField] private BoxCollider2D blockingShape;

        public string Id => SelfData.Name;

        private NestPathRoadNode[] nodes;
        private NestPathLocalEdge[] edges;

        public override Collider2D GetBlockingShape()
        {
            return blockingShape;
        }

        public override bool HasPathNode => true;

        public override IEnumerable<NestPathNode> GetNodes()
        {
            return nodes;
        }

        public override IEnumerable<NestPathLocalEdge> GetLocalEdges()
        {
            return edges;
        }

        protected override void OnInitialized()
        {
            //BlockingShapeの形成
            if (!blockingShape) blockingShape = GetComponent<BoxCollider2D>();
            if (!blockingShape) blockingShape = gameObject.AddComponent<BoxCollider2D>();

            blockingShape.size = SelfData.BlockingShape.size;
            blockingShape.offset = SelfData.BlockingShape.position;

            //PathNodeの形成
            nodes = SelfData.NodeData.Select(n => new NestPathRoadNode(this, n.Position, n.Name)).ToArray();
            edges = SelfData.EdgeData.Select(e => new NestPathLocalEdge(nodes[e.IndexA], nodes[e.IndexB])).ToArray();
        }
    }
}