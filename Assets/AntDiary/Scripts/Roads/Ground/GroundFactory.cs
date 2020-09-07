using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class GroundFactory : NestElementFactory<GroundData>
    {
        [SerializeField] private GameObject groundPrefab = default;

        public override NestElement InstantiateNestElement(GroundData elementData)
        {
            var go = Instantiate(groundPrefab, elementData.Pos,
                Quaternion.identity);
            var ne = go.GetComponent<Ground>();
            ne.Initialize(elementData);
            return ne;
        }
    }
}