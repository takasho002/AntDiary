using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class QueenAntRoom : Room<QueenAntRoomData>
    {
        [SerializeField] private Collider2D blockingShape = default;
        [SerializeField] private Rect boundingRect = default;

        [SerializeField] protected float constructCost = 0;

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
        protected override void OnBuildingCompleted()
        {/*
            QueenAntData queen = new QueenAntData() { Position = transform.position };
            NestSystem.Instance.InstantiateAnt(queen);*/
        }

        protected override void OnInitialized()
        {
            Vector2 center = new Vector2(boundingRect.x, boundingRect.y);
            Vector2 x = new Vector2(boundingRect.width * 0.5f, 0);
            Vector2 y = new Vector2(0, boundingRect.height * 0.5f);
            nodes = new[]
            {
                new NestPathRoomNode(this, center),
                new NestPathRoomNode(this, center + x, "right"),
                new NestPathRoomNode(this, center + y, "top"),
                new NestPathRoomNode(this, center - x, "left"),
                new NestPathRoomNode(this, center - y, "bottom"),
            };

            edges = new[]
            {
                new NestPathLocalEdge(nodes[0], nodes[1]),
                new NestPathLocalEdge(nodes[0], nodes[2]),
                new NestPathLocalEdge(nodes[0], nodes[3]),
                new NestPathLocalEdge(nodes[0], nodes[4]),
                
                new NestPathLocalEdge(nodes[1], nodes[2]),
                new NestPathLocalEdge(nodes[2], nodes[3]),
                new NestPathLocalEdge(nodes[3], nodes[4]),
                new NestPathLocalEdge(nodes[4], nodes[1]),
            };
            QueenAntData queen = new QueenAntData() { Position = transform.position };
            NestSystem.Instance.InstantiateAnt(queen);
        }

        public override float RequiredResources => constructCost;
    }
}