using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace AntDiary
{
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
            base.SeasonalChange(() => changeBackground.SetBackground(seasonId), () => fadeOut.BGMsystem("test2"));
        }
    }
}