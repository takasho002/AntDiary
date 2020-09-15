using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class GeneralPathRoadFactory : NestElementFactory<GeneralPathRoadData>
    {
        [SerializeField] private GameObject generalPathRoadPrefab;

        public override NestElement InstantiateNestElement(GeneralPathRoadData elementData)
        {
            var obj = Instantiate(generalPathRoadPrefab, elementData.Position, Quaternion.identity);
            var ne = obj.GetComponent<GeneralPathRoad>();
            ne.Initialize(elementData);
            return ne;
        }
    }
}