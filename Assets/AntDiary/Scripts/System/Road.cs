using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public abstract class Road<T> : NestBuildableElement<T> where T : RoadData{
        /// <summary>
        /// 建築にかかるコスト
        /// </summary>
        [SerializeField] protected float constructCost = 15;
        
        /// <summary>
        /// おけるかどうかの判定に使うCollider
        /// </summary>
        [SerializeField] protected Collider2D blockingShape;
        
        public override Collider2D GetBlockingShape(){
            return blockingShape;
        }

        public override float RequiredResources => constructCost;
        
        
    }
}