using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class StoreRoomFactory : NestElementFactory<StoreRoomData>
    {
        [SerializeField] private GameObject storeRoomPrefab = default;

        public override NestElement InstantiateNestElement(StoreRoomData elementData)
        {
            var r = Instantiate(storeRoomPrefab, elementData.Position, Quaternion.identity)
                .GetComponent<StoreRoom>();
            r.Initialize(elementData);
            return r;
        }
    }
}