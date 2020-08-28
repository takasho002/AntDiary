using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    public class NestBuildableElementData : NestElementData
    {
        //Keyは10~19
        [Key(10)] public bool IsUnderConstruction { get; set; } = false;
        
        /// <summary>
        /// 建築の進捗
        /// </summary>
        [Key(11)] public float BuildingProgress { get; set; } = 0;
    }
}