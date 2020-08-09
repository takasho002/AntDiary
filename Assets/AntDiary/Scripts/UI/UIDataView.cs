using System;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private Text wholeCountText;

        [SerializeField] private Image[] chartLayers;
        [SerializeField] private Text antCountText;

        [SerializeField] private Text childrenCountText;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
            
            
            
            
            
        }
    }
}