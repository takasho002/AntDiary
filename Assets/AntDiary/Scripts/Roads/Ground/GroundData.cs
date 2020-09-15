using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class GroundData : RoadData
    {
        //Keyは30~
        [Key(30)] public Vector2 Pos { get; set; }
    }
}