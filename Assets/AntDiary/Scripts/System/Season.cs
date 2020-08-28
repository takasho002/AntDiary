using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace AntDiary
{
    // 季節のクラス
    // これ使って四季作る
    public class Season : MonoBehaviour
    {
        // 季節のID(春:0, 夏:1, 秋:2, 冬3)
        protected int seasonId;
        // 最後にこの季節だった時間
        // これをもとにシーズン切り替わりを判定
        // 初期値: -1*SeasonInterval
        private float lastTimeInThisSeason;

        /* 初期化
         * 季節とlastTimeInThisSeasonの初期値設定
         */
        public void Initialize(int _seasonId)
        {
            seasonId = _seasonId;
            lastTimeInThisSeason = -1 * SeasonInterval;
        }

        /* 季節変化の確認と必要な関数の実行
         * _callDrawBG: 背景描画の関数(Actionとして)
         * _callPlayBGM: BGMの再生の関数(Actionとして)
         * Actionとして渡すには，通常の関数をラムダで囲えばOK
         */
        public void SeasonalChange(Action _callDrawBG, Action _callPlayBGM)
        {
            UnityEngine.Debug.Log("な");
            // HACK: Actionじゃなくて直接関数渡したい
            if (isInThisSeason)
            {
             UnityEngine.Debug.Log("なん");
                if (isSeasonalChange)
                {
                    _callDrawBG();
                    _callPlayBGM();
                    UnityEngine.Debug.Log("なんじゃ");
                }
                lastTimeInThisSeason = CurrentTime;
            }
        }

        private void Update()
        {
            UnityEngine.Debug.Log("n");
        }

        // 長いんで呼びやすく
        private float CurrentTime => TimeSystem.Instance.CurrentTime;
        private float SeasonInterval => TimeSystem.Instance.SeasonInterval;
        // lastTimeInThisSeasonとCurrentTimeの差
        private float DeltaTimeFromCurrentTime => CurrentTime - lastTimeInThisSeason;

        // 季節が一致しているか(一致: true)
        public Boolean isInThisSeason => TimeSystem.Instance.CurrentSeason == seasonId;
        // 季節が変わったか(変わった: true)
        public Boolean isSeasonalChange => DeltaTimeFromCurrentTime >= SeasonInterval;
        [SerializeField] protected ChangeBackground changeBackground;
        [SerializeField] protected FadeOut fadeOut;

    }

    // 夏
    public class Summer : Season
    {
        void Start()
        {
            // 夏はseaonIdが1
            base.Initialize(1);
        }

        void Update()
        {
            base.SeasonalChange(()=>changeBackground.SetBackground(seasonId), ()=>fadeOut.BGMsystem("test2"));
        }
    }

    // 秋
    public class Autumn : Season
    {
        void Start()
        {
            // 秋はseasonIdが2
            base.Initialize(2);
        }

        void Update()
        {
            base.SeasonalChange(() => changeBackground.SetBackground(seasonId), () => fadeOut.BGMsystem("test1"));
        }
    }

}