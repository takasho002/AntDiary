using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class GeneSystem
    {
        #region Singleton Implementation

        public static GeneSystem Instance { get; private set; }

        /// <summary>
        /// 自身をSingletonのインスタンスとして登録。既に別のインスタンスが存在する場合はfalseを返す。
        /// </summary>
        /// <returns></returns>
        private bool RegisterSingletonInstance()
        {
            if (Instance == null)
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

        public GeneSystem()
        {
            if (!RegisterSingletonInstance())
            {
                throw new InvalidOperationException("Duplicate GeneSystem singleton instance");
            }
        }
    }
}