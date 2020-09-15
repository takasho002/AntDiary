using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class DebugAntCommonData : AntCommonData<DebugAntData>
    {
        //Keyは10~
    }
}