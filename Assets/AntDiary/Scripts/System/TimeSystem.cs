using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// 時間経過システム
    /// TimeManager.Instanceでシングルトンのインスタンスにアクセス可能
    /// </summary>
    public class TimeSystem : MonoBehaviour, IDebugMenu
    {
        #region Singleton Implementation

        public static TimeSystem Instance { get; private set; }

        /// <summary>
        /// 自身をSingletonのインスタンスとして登録。既に別のインスタンスが存在する場合はfalseを返す。
        /// </summary>
        /// <returns></returns>
        private bool RegisterSingletonInstance()
        {
            if (!Instance)
            {
                Instance = this;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        private void Awake()
        {
            if (!RegisterSingletonInstance())
            {
                Destroy(gameObject);
                return;
            }
        }

        [SerializeField] private TimeSystemUpdateMode updateMode = TimeSystemUpdateMode.Scaled;
        [SerializeField] private bool playOnLoad = true;

        /// <summary>
        /// 季節更新のインターバル (秒)
        /// </summary>
        [SerializeField] private float seasonInterval = 300;

        public float SeasonInterval => seasonInterval;
        public float MonthInterval => SeasonInterval / 3;


        private float SourceTime
        {
            get
            {
                switch (updateMode)
                {
                    case TimeSystemUpdateMode.Scaled:
                        return Time.time;
                        
                    case TimeSystemUpdateMode.Unscaled:
                        return Time.unscaledTime;
                        
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private float SourceDeltaTime
        {
            get
            {
                switch (updateMode)
                {
                    case TimeSystemUpdateMode.Scaled:
                        return Time.deltaTime;
                        
                    case TimeSystemUpdateMode.Unscaled:
                        return Time.unscaledDeltaTime;
                        
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// セーブデータからロードしたゲーム内時刻
        /// </summary>
        private float timeOffset = 0;

        /// <summary>
        /// 時刻が開始した時刻 (SourceTime)
        /// </summary>
        private float startTime;

        /// <summary>
        /// 停止した時刻 (SourceTime)
        /// </summary>
        private float stoppedTime;

        /// <summary>
        /// 停止した総時間 (SourceTime)
        /// </summary>
        private float stoppedDuration = 0;

        /// <summary>
        /// 現在のゲーム内時刻 (秒)
        /// </summary>
        public float CurrentTime
        {
            get
            {
                if (IsPlaying)
                {
                    return SourceTime - startTime - stoppedDuration + timeOffset;
                }
                else
                {
                    return stoppedTime - startTime - stoppedDuration + timeOffset;
                }
            }
        }


        public int CurrentSeason => Mathf.FloorToInt(CurrentTime / SeasonInterval) % 4;
        public int CurrentMonth => Mathf.FloorToInt(CurrentTime / MonthInterval) % 12;

        public IObservable<int> OnSecondUpdate =>
            this.UpdateAsObservable().Select(_ => Mathf.FloorToInt(CurrentTime)).DistinctUntilChanged()
                .TakeUntilDestroy(this);

        public IObservable<int> OnSeasonUpdate => OnSecondUpdate.Select(_ => CurrentSeason).DistinctUntilChanged();
        public IObservable<int> OnMonthUpdate => OnSecondUpdate.Select(_ => CurrentMonth).DistinctUntilChanged();

        /// <summary>
        /// 再生中かどうか
        /// </summary>
        public bool IsPlaying { get; private set; } = false;

        private void Play()
        {
            if (IsPlaying) return;
            stoppedDuration += SourceTime - stoppedTime;
            IsPlaying = true;
        }

        private void Stop()
        {
            if (!IsPlaying) return;
            stoppedTime = SourceTime;
            IsPlaying = false;
        }

        private void LoadTime()
        {
            if (SaveUnit.Current != null)
            {
                timeOffset = GameContext.Current.s_CurrentTime;
            }
            else
            {
                Debug.LogError("TimeSystem: SaveUnitがロードされていないため、時刻をロードできません。");
            }
            startTime = SourceTime;
            stoppedTime = SourceTime;
            stoppedDuration = 0;
            if (IsPlaying)
                Play();
        }

        private void Start()
        {
            //セーブ直前に時刻情報を更新するようにセッティング
            
            if (SaveUnit.Current != null)
            {
                //Startの時点でセーブデータがロードされていれば、読み込み済みのSaveUnitに対してセッティング
                SaveUnit.Current.OnBeforeSave.Subscribe(su =>
                {
                    su.s_GameContext.s_CurrentTime = CurrentTime;
                    LoadTime();
                });
                //時刻をロード
                LoadTime();
                if (playOnLoad)
                    Play();
            }
            
            //次回以降のロードでも行われるようにする
            SaveUnit.OnCurrentSaveUnitChanged.Subscribe(_ => { 
                SaveUnit.Current.OnBeforeSave.Subscribe(su =>
                {
                    su.s_GameContext.s_CurrentTime = CurrentTime;
                    LoadTime();
                });
                LoadTime();
                if (playOnLoad)
                    Play();
            });
        }

        private void Update()
        {
        }

        #region Debug

        public string pageTitle { get; } = "時刻システム";
        public void OnGUIPage()
        {
            GUILayout.Label($"現在時刻: {CurrentTime}");
            GUILayout.Label($"季節: {CurrentSeason}");
            GUILayout.Label($"月: {CurrentMonth}");
            GUILayout.Label($"IsPlaying: {IsPlaying}");
            GUILayout.Label($"timeOffset: {timeOffset}");
            GUILayout.Label($"startTime: {startTime}");
            GUILayout.Label($"stoppedTime: {stoppedTime}");
            GUILayout.Label($"stoppedDuration: {stoppedDuration}");
        }
        #endregion
    }

    public enum TimeSystemUpdateMode
    {
        /// <summary>
        /// Time.timeScaleの影響を受ける。
        /// </summary>
        Scaled,

        /// <summary>
        /// Time.timeScaleの影響を受けない。
        /// </summary>
        Unscaled
    }
}