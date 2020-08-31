using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace AntDiary
{
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