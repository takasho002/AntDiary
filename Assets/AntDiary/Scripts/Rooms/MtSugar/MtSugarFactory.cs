using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class MtSugarFactory : NestElementFactory<MtSugarData>
    {
        [SerializeField] private GameObject mtSugarPrefab = default;

        public override NestElement InstantiateNestElement(MtSugarData elementData)
        {
            var r = Instantiate(mtSugarPrefab, elementData.Position, Quaternion.identity)
                .GetComponent<MtSugar>();
            r.Initialize(elementData);
            return r;
        }
    }
}