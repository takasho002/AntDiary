using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class DebugAntData : AntData
    {
        [Key(100)]
        public string DummyData { get; set; }
    }
}