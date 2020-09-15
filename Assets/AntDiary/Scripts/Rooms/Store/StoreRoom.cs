using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class StoreRoom : Room<StoreRoomData>
    {
        [SerializeField] private Collider2D blockingShape = default;
        [SerializeField] private Rect boundingRect = default;

        private NestPathNode[] nodes;
        private NestPathLocalEdge[] edges;

        //ふりゾーン
        [SerializeField] private Sprite Esaari;
        [SerializeField] private Sprite Esanashi;

        private int [] season = {1,1,1,1};
        //ここまで

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

            //ふりゾーン
            //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            // 貯蓄庫の初期の貯蓄量を0とする
            NestSystem.Instance.Data.StoredFood = 0;
            //ここまで
        }
        public override float RequiredResources { get; }

        //ふりゾーン
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject colliderObj = collision.gameObject;
            if (!colliderObj.GetComponent<ErgateAnt>().GetType().Name.Equals("ErgateAnt")) return;
            ErgateAnt script = (ErgateAnt)colliderObj.GetComponent<ErgateAnt>();
            Debug.Log(colliderObj.GetComponent<ErgateAnt>().GetType().Name);

            if (script.IsHoldingFood && !SelfData.IsUnderConstruction)
            {
                // 貯蓄部屋のストックが0から増える際はspriteを餌ありに変更
                if (NestSystem.Instance.Data.StoredFood == 0)
                {
                    spriteRenderer.sprite = Esaari;
                }
                NestSystem.Instance.Data.StoredFood += (int)(season[TimeSystem.Instance.CurrentSeason%4] * script.Capacity);
                Debug.Log(NestSystem.Instance.Data.StoredFood);
                //script.IsHoldingFood = false;
            }
        }

    }
}