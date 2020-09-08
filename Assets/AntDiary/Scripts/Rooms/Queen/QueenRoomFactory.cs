using System;
using System.Collections;
using System.Collections.Generic;
using AntDiary;
using UnityEngine;

namespace AntDiary
{
    public class QueenRoomFactory : NestElementFactory<QueenRoomData>
    {
        [SerializeField] private GameObject queenRoomPrefab;
        public override NestElement InstantiateNestElement(QueenRoomData elementData)
        {
            var o = Instantiate(queenRoomPrefab, elementData.Position, Quaternion.identity);
            var ne = o.GetComponent<QueenRoom>();
            if(ne == null) throw new NullReferenceException();

            ne.Initialize(elementData);
            return ne;
        }
    }
}