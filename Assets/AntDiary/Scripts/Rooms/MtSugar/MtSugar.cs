using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AntDiary
{
    public class MtSugar : Room<MtSugarData>
    {
        [SerializeField] private Collider2D blockingShape = default;
        [SerializeField] private Rect boundingRect = default;

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
            Vector2 center = new Vector2(boundingRect.x, boundingRect.y);
            Vector2 x = new Vector2(boundingRect.width * 0.5f, 0);
            Vector2 y = new Vector2(0, boundingRect.height * 0.5f);
            nodes = new[]
            {
                new NestPathRoomNode(this, center),
                new NestPathRoomNode(this, center+x,"joint")
            };
            
            edges = new[]
            {
                new NestPathLocalEdge(nodes[0],nodes[1])
            };
            
        }

        public override float RequiredResources { get; }
    }
}