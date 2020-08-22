using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class DebugRoadData : RoadData
    {
        //Keyは30~
        [Key(30)] public Vector2 From { get; set; }
        [Key(31)] public Vector2 To { get; set; }
    }
}