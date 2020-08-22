using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public abstract class NestBuildableElement<T> : NestElement<T> where T : NestBuildableElementData
    {
        /// <summary>
        /// 建築の完了に必要な労働コスト
        /// </summary>
        public abstract float RequiredResources { get; }
        public bool IsUnderConstruction
        {
            get => SelfData.IsUnderConstruction;
        }

        /// <summary>
        /// 建築のために労働コストを投入する。
        /// </summary>
        public void Commit(float buildingResource)
        {
            if (!IsUnderConstruction) return;

            SelfData.BuildingProgress += buildingResource;
            if (SelfData.BuildingProgress >= RequiredResources)
            {
                SelfData.IsUnderConstruction = false;
                SelfData.BuildingProgress = 0;
                OnBuildingCompleted();
            }
        }
        
        /// <summary>
        /// 建築が完了したときに呼ばれる。
        /// </summary>
        protected virtual void OnBuildingCompleted()
        {}
        
    }
}