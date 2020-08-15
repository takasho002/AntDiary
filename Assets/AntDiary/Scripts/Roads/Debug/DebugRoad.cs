using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class DebugRoad : NestElement<DebugRoadData>
    {
        [SerializeField] private Collider2D blockingShape;
        
        private NestPathNode[] nodes;
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
            nodes = new[]
            {
                new NestPathRoadNode(this, SelfData.From - (Vector2) transform.position, "plain"),
                new NestPathRoadNode(this, SelfData.To - (Vector2) transform.position, "plain")
            };

            edges = new[]
            {
                new NestPathLocalEdge(nodes[0], nodes[1])
            };
        }
    }
}