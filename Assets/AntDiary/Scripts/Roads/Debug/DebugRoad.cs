using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class DebugRoad : Road<DebugRoadData>
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
            var position = transform.position;
            nodes = new[]
            {
                new NestPathRoadNode(this, SelfData.From - (Vector2) position, "wild_a"),
                new NestPathRoadNode(this, SelfData.To - (Vector2) position, "wild_b")
            };

            edges = new[]
            {
                new NestPathLocalEdge(nodes[0], nodes[1])
            };
        }

        public override float RequiredResources { get; }
    }
}