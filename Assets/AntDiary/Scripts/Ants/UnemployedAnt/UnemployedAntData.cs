using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class UnemployedAntData : AntData
    {
        // FIXME: DebugAntをそのままコピーしただけ
        [Key(101)]
        public string DummyData { get; set; }
    }
}
