using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class ChochikukoRoomFactory : NestElementFactory<ChochikukoRoomData>
    {
        [SerializeField] private GameObject debugRoomPrefab = default;

        public override NestElement InstantiateNestElement(ChochikukoRoomData elementData)
        {
            var r = Instantiate(debugRoomPrefab, elementData.Position, Quaternion.identity)
                .GetComponent<ChochikukoRoom>();
            r.Initialize(elementData);
            return r;
        }
    }
}