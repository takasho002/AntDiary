using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class Ground : Road<GroundData>
    {
        [SerializeField] private Collider2D blockingShape = default;
        
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
                new NestPathRoadNode(this, new Vector2(0.5f,0), "body"),
                new NestPathRoadNode(this, new Vector2(0.5f,0), "wild_a"),
                new NestPathRoadNode(this, new Vector2(1.0f,0), "wild_b"),
                new NestPathRoadNode(this, new Vector2(1.5f,0), "wild_c"),
                new NestPathRoadNode(this, new Vector2(2.0f,0), "wild_d"),
                new NestPathRoadNode(this, new Vector2(2.5f,0), "wild_e"),
                new NestPathRoadNode(this, new Vector2(3.0f,0), "wild_f")
            };

            edges = new[]
            {
                new NestPathLocalEdge(nodes[0], nodes[1]),
                new NestPathLocalEdge(nodes[1], nodes[2]),
                new NestPathLocalEdge(nodes[2], nodes[3]),
                new NestPathLocalEdge(nodes[3], nodes[4]),
                new NestPathLocalEdge(nodes[4], nodes[5]),
                new NestPathLocalEdge(nodes[5], nodes[6])
            };
        }

        public override float RequiredResources { get; }
    }
}