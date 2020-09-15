using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [Union(0, typeof(DebugAntData))]
    [Union(1, typeof(UnemployedAntData))]
    [Union(2, typeof(ErgateAntData))]
    [Union(3, typeof(BuilderAntData))]
    [Union(4, typeof(QueenAntData))]
    [Union(5, typeof(EnemyAntData))]
    [Union(6, typeof(SoldierAntData))]
    public abstract class AntData
    {
        //方針として、AntDataのメンバ(アリの複数種に共通値)のKeyは0 ~ 99, 各継承クラスのメンバのKeyは100~にしたいな～と

        [Key(10)] public Vector2 Position { get; set; }

        [Key(11)] public bool IsAlive { get; set; } = true;
        
        [Key(12)] public float Health { get; set; }
        
        
    }
}