using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// セーブデータなどのうち、ゲームプレイに関する情報を保持する。
    /// GameContext.Currentで現在のセーブデータのGameContextにアクセス可能。
    /// </summary>
    [MessagePackObject()]
    public class GameContext
    {
        /// <summary>
        /// 現在アクティブなGameContext。
        /// </summary>
        public static GameContext Current => SaveUnit.Current?.s_GameContext;

        [Key(10)] public float s_CurrentTime { get; set; }
        [Key(11)] public NestData s_NestData { get; set; }
    }
}