using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// 巣の初期状態を定義するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "DefaultEnvironmentData", menuName = "AntDiary/DefaultEnvironmentData")]
    public class DefaultEnvironmentData : ScriptableObject
    {
        [SerializeField] private GeneralPathRoadData[] generalPathRoads;
        public GeneralPathRoadData[] GeneralPathRoads => generalPathRoads;
    }
}