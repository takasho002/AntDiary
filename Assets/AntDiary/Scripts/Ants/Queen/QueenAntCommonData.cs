using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class QueenAntCommonData : AntCommonData<QueenAntData>
    {
        public QueenAntCommonData()
        {
            MaxHealth = 10.0f;
        }
        //Keyは10~
    }
}