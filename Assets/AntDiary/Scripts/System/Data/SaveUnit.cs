using System;
using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UniRx;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// セーブデータのルート。あらゆるデータはここに吊り下げる。
    /// SaveUnit.Currentで現在のセーブデータにアクセス可能。
    /// </summary>
    [MessagePackObject()]
    public class SaveUnit : IMessagePackSerializationCallbackReceiver
    {
        /// <summary>
        /// 現在アクティブなSaveUnit。
        /// </summary>
        public static SaveUnit Current
        {
            get => current;
            private set
            {
                current = value;
                onCurrentSaveUnitChanged.OnNext(current);
            }
        }

        private static SaveUnit current;
        
        /// <summary>
        /// 現在のSaveUnitとして設定。
        /// </summary>
        public void SetAsCurrent()
        {
            Current = this;
        }
        
        /// <summary>
        /// CurrentのSaveUnitが変更されたときに呼ばれる。
        /// </summary>
        [IgnoreMember] public static IObservable<SaveUnit> OnCurrentSaveUnitChanged => onCurrentSaveUnitChanged;  
        private static readonly Subject<SaveUnit> onCurrentSaveUnitChanged = new Subject<SaveUnit>();

        /// <summary>
        /// 初期状態のセーブデータを生成して返す。
        /// </summary>
        /// <returns></returns>
        public static SaveUnit GetDefaultSaveUnit()
        {
            var su = new SaveUnit()
            {
                Version = "debug",
                s_GameContext = new GameContext()
                {
                    s_NestData = new NestData()
                }
            };
            return su;
        }

        /// <summary>
        /// セーブされる直前に通知するストリーム。
        /// </summary>
        [IgnoreMember] public IObservable<SaveUnit> OnBeforeSave => onBeforeSave;  
        private readonly Subject<SaveUnit> onBeforeSave = new Subject<SaveUnit>();
        public void OnBeforeSerialize()
        {
            onBeforeSave.OnNext(this);
        }

        /// <summary>
        /// ロードされた直後に通知するストリーム。
        /// </summary>
        [IgnoreMember] public IObservable<SaveUnit> OnAfterLoad => onAfterLoad;  
        private readonly Subject<SaveUnit> onAfterLoad = new Subject<SaveUnit>();
        public void OnAfterDeserialize()
        {
            onAfterLoad.OnNext(this);
        }
        
        /// <summary>
        /// セーブデータフォーマットのバージョン。アップデート等でセーブデータの構造が変化した際、適切に新バージョンに移行するために使用。
        /// </summary>
        [Key(0)]
        public string Version { get; set; }
        
        //以下、データ類。とりあえず「s_」プレフィックスつけてみたりしている
        [Key(10)]
        public GameContext s_GameContext { get; set; }
        
        //セーブデータごとの設定などがあればこれ以下に持たせたい
    }
}