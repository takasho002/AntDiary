using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class QueenAntRoomFactory : NestElementFactory<QueenAntRoomData>
    {
        [SerializeField] private GameObject queenAntRoomPrefab = default;

        public override NestElement InstantiateNestElement(QueenAntRoomData elementData)
        {
            var r = Instantiate(queenAntRoomPrefab, elementData.Position, Quaternion.identity)
                .GetComponent<QueenAntRoom>();
            r.Initialize(elementData);
            return r;
        }
    }
}