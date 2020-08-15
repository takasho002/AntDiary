using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class DebugRoadData : NestElementData
    {
        [Key(0)] public Vector2 From { get; set; }
        [Key(1)] public Vector2 To { get; set; }
    }
}