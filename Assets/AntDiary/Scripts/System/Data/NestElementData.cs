using System;
using System.Collections;
using System.Collections.Generic;
using AntDiary.Scripts.Roads;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [Serializable]
    [Union(0, typeof(DebugRoomData))]
    [Union(1, typeof(DebugRoadData))]
    [Union(2, typeof(StoreRoomData))]
    [Union(3, typeof(IShapeRoadData))]
    [Union(4, typeof(LShapeRoadData))]
    [Union(5, typeof(CrossShapeRoadData))]
    [Union(6, typeof(TShapeRoadData))]
    [Union(7, typeof(QueenRoomData))]
    [Union(8, typeof(ChochikukoRoomData))]
    public abstract class NestElementData
    {
        [SerializeField] private string guid = System.Guid.NewGuid().ToString();
        [SerializeField] private Vector2 position;

        public NestElementData()
        {
        }

        //Keyは0~9を使用することにします。足りなくなったらまた考えるので@rucchoまで。
        [Key(0)]
        public string Guid
        {
            get => guid;
            set => guid = value;
        }

        [Key(1)]
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
    }
}