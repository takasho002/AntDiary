using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    public class QueenRoom : Room<QueenRoomData>
    {
        [SerializeField] private Collider2D blockingShape;
        [SerializeField] private Rect boundingRect = default;

        private NestPathRoomNode[] nodes;
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

        public override float RequiredResources => 1;

        protected override void OnInitialized()
        {
            base.OnInitialized();
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
            if(!IsUnderConstruction)GetComponent<AntSpawner>().StartSpawn();
        }

        private void OnDrawGizmos()
        {
            Vector2 center = new Vector2(boundingRect.x, boundingRect.y);
            Vector2 x = new Vector2(boundingRect.width * 0.5f, 0);
            Vector2 y = new Vector2(0, boundingRect.height * 0.5f);
            Gizmos.DrawWireCube((Vector2)transform.position + center, new Vector3(boundingRect.width, boundingRect.height, 1));
        }

        protected override void OnBuildingCompleted()
        {
            base.OnBuildingCompleted();
            //建築完了時に女王アリを生成
            QueenAntData queenantdata = new QueenAntData() { Position = transform.position};
            NestSystem.Instance.InstantiateAnt(queenantdata);
            GetComponent<AntSpawner>().StartSpawn();
        }

    }
    
    public static class NestSystemExtensionsQueenRoom
    {
        /// <summary>
        /// 女王部屋を取得します。ない場合はnullが返ります。
        /// </summary>
        /// <param name="nestSystem"></param>
        /// <returns></returns>
        public static QueenRoom GetQueenRoom(this NestSystem nestSystem)
        {
            return nestSystem.NestElements.OfType<QueenRoom>().FirstOrDefault();
        }
    }
}