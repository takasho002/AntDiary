using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class BuilderAntCommonData : AntCommonData<BuilderAntData>
    {
        //Keyは10~
    }
}