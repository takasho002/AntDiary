using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject()]
    public class ErgateAntCommonData : AntCommonData<ErgateAntData>
    {
        public ErgateAntCommonData()
        {
            Capacity = 5;
        }

        //Keyは10~
        [Key(10)]
        public int Capacity { get; set; }
    }
}