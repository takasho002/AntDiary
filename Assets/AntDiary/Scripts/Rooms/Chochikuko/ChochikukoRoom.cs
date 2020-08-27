using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class ChochikukoRoom : Room<ChochikukoRoomData>
    {
        [SerializeField] private Collider2D blockingShape = default;
        [SerializeField] private Rect boundingRect = default;

        // デバッグ用にアリのステータスを指定
        [SerializeField] private int status;

        // 餌あり，餌なしのスプライトを登録
        [SerializeField] private Sprite Esaari;
        [SerializeField] private Sprite Esanashi;

        private SpriteRenderer spriteRenderer;

        private NestPathNode[] nodes;
        private NestPathLocalEdge[] edges;

        public override Collider2D GetBlockingShape()
        {
            return blockingShape;
        }

        public override bool HasPathNode => true;

        public override float RequiredResources => throw new NotImplementedException();

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
                //new NestPathRoomNode(this, center),
                new NestPathRoomNode(this, center + x, "right"),
                new NestPathRoomNode(this, center + y, "top"),
                new NestPathRoomNode(this, center - x, "left"),
                new NestPathRoomNode(this, center - y, "bottom"),
            };

            edges = new[]
            {
                /*new NestPathLocalEdge(nodes[0], nodes[1]),
                new NestPathLocalEdge(nodes[0], nodes[2]),
                new NestPathLocalEdge(nodes[0], nodes[3]),
                new NestPathLocalEdge(nodes[0], nodes[4]),*/
                new NestPathLocalEdge(nodes[1], nodes[2]),
                new NestPathLocalEdge(nodes[2], nodes[3]),
                new NestPathLocalEdge(nodes[3], nodes[0]),
                new NestPathLocalEdge(nodes[0], nodes[1]),
            };

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject colliderObj = collision.gameObject;
            // DebugaAntMoveByKeyはデバッグ用のクラス名，のちにAntDataに変更
            // scriptは仮の変数名，isHoldingFoodのbool値が取得できるスクリプトを登録
            DebugAntMoveByKey script = colliderObj.GetComponent<DebugAntMoveByKey>();

            if (script.isHoldingFood)
            {
                // 貯蓄部屋のストックが0から増える際はspriteを餌ありに変更
                if (NestSystem.Instance.Data.StoredFood == 0)
                {
                    spriteRenderer.sprite = Esaari;
                }
                var currentSeason = TimeSystem.Instance.CurrentSeason;
                NestSystem.Instance.Data.StoredFood += currentSeason * status;
            }
        }
    }
}