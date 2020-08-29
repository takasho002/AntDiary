using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AntDiary
{
    /// <summary>
    /// データUI用システム
    /// </summary>
    public class UIDataView : MonoBehaviour
    {
        private NestData NestData => NestSystem.Instance?.Data;

        [SerializeField] private bool updateEveryFrame = true;

        /// <summary>
        /// アリの総数を示す uGUI Text。
        /// </summary>
        [SerializeField] private Text wholeCountText;

        [SerializeField] private string wholeTextFormat = "総数: {0}";

        /// <summary>
        /// アリの種類の割合を示す円グラフに使用するuGUI Image (ModeをFilledに指定)
        /// </summary>
        [SerializeField] private Image[] chartLayers;

        /// <summary>
        /// アリの数を示すuGUI Text。
        /// </summary>
        [SerializeField] private Text antCountText;

        /// <summary>
        /// 子どもの数を示す uGUI Text。ただしデータが存在しないためダミー。
        /// </summary>
        [SerializeField] private Text childrenCountText;

        /// <summary>
        /// 女王の情報を示す uGUI Text。ただしデータが存在しないためダミー。
        /// </summary>
        [SerializeField] private Text queenText;

        [SerializeField] private UIDataViewAntTypeDictionary antTypeDictionary;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (updateEveryFrame)
                UpdateView();
        }

        private Dictionary<Type, int> antCounter = new Dictionary<Type, int>();

        public void UpdateView()
        {
            if (NestData == null) return;

            //アリの数をカウント
            antCounter.Clear();
            int wholeCount = 0;
            int dead = 0;
            foreach (var ant in NestData.Ants)
            {
                wholeCount++;
                Type antType = ant.GetType();
                if (!antCounter.ContainsKey(ant.GetType()))
                {
                    antCounter.Add(antType, 1);
                }
                else
                {
                    antCounter[antType]++;
                }

                if (!ant.IsAlive) dead++;
            }

            //総数
            wholeCountText.text = string.Format(wholeTextFormat, wholeCount.ToString());

            //種類ごとの数、円グラフ
            float sum_r = 1f;
            if (chartLayers.Length < antCounter.Count)
            {
                Debug.LogWarning("UIDataView: chatLayersの数が不足しています。");
            }

            foreach (var l in chartLayers) l.fillAmount = 0f;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var antType in antCounter)
            {
                var entry = antTypeDictionary.GetEntry(antType.Key);
                if (entry != null)
                {
                    sb.AppendLine(
                        $"<color=#{ColorUtility.ToHtmlStringRGB(entry.ChartColor)}>■</color> {entry.DisplayName}: {antType.Value}");
                }
                else
                {
                    sb.AppendLine(
                        $"<color=#000000>■</color> {antType.Key.Name}: {antType.Value}");
                }

                float r = (float) antType.Value / wholeCount;
                if (i < chartLayers.Length)
                {
                    //chartLayers[i].color = entry?.ChartColor ?? Color.black;    
                    chartLayers[i].fillAmount = sum_r;
                }

                sum_r -= r;
                i++;
            }

            antCountText.text = sb.ToString();
        }
    }

    [Serializable]
    public class UIDataViewAntTypeDictionary
    {
        [SerializeField] private List<UIDataViewAntTypeEntry> entries = new List<UIDataViewAntTypeEntry>()
        {
            new UIDataViewAntTypeEntry()
            {
                ChartColor = Color.white,
                DisplayName = "デバッグ",
                TypeName = "DebugAntData"
            }
        };

        public UIDataViewAntTypeEntry GetEntry(Type type)
        {
            foreach (var e in entries)
            {
                if (e.Match(type)) return e;
            }

            return null;
        }
    }

    [Serializable]
    public class UIDataViewAntTypeEntry
    {
        /// <summary>
        /// AntDataの型名。DebugAntDataとか。
        /// </summary>
        [SerializeField] private string typeName;

        /// <summary>
        /// 表示名。
        /// </summary>
        [SerializeField] private string displayName;

        /// <summary>
        /// 円グラフの色。
        /// </summary>
        [SerializeField] private Color chartColor;

        public string TypeName
        {
            get => typeName;
            set => typeName = value;
        }

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public Color ChartColor
        {
            get => chartColor;
            set => chartColor = value;
        }


        public bool Match(Type targetType)
        {
            return targetType.Name == typeName;
        }
    }
}