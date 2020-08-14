using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class NestDebugRoomFactory : NestElementFactory<NestDebugRoomData>
    {
        [SerializeField]
        private GameObject debugRoomPrefab;
        public override NestElement InstantiateNestElement(NestDebugRoomData elementData)
        {
            var r = Instantiate(debugRoomPrefab, elementData.Position, Quaternion.identity).GetComponent<NestDebugRoom>();
            r.Initialize(elementData);
            return r;
        }
    }
}