using System.Collections;
using System.Collections.Generic;
using AntDiary;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [Union(0, typeof(DebugAntCommonData))]
    [Union(1, typeof(BuilderAntCommonData))]
    [Union(2, typeof(ErgateAntCommonData))]
    [Union(3, typeof(UnemployedAntCommonData))]
    [Union(4, typeof(QueenAntCommonData))]
    public abstract class AntCommonDataBase
    {
        //派生クラスのメンバのKeyは10~

        [Key(0)] public float MaxHealth { get; set; }
        [Key(1)] public float BasicMovementSpeed { get; set; }
        [Key(2)] public float BasicConsumeAmount { get; set; }
        [Key(3)] public float BasicEfficiency { get; set; }
    }

    /// <summary>
    /// アリの種類に対して共通のパラメータを保持します。
    /// ゲーム開始時のデフォルト値を設定するにはコンストラクタをオーバーライドして実装してください。
    /// </summary>
    public class AntCommonData<T> : AntCommonDataBase where T : AntData
    {
        public AntCommonData()
        {
            MaxHealth = 1f;
            BasicMovementSpeed = 1f;
            BasicConsumeAmount = 1f;
            BasicEfficiency = 1f;
        }
    }
}