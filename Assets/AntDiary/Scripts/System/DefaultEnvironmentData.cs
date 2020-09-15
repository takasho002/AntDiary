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
        public GeneralPathRoadData[] GeneralPathRoads => generalPathRoads;
        [SerializeField] private GeneralPathRoadData[] generalPathRoads;

        public Vector2 InitialRoadPosition => initialRoadPosition;
        [SerializeField] private Vector2 initialRoadPosition;

        public string InitialRoadNodeName => initialRoadNodeName;
        [SerializeField]private string initialRoadNodeName;
        
        public string InitialRoadBindNodePath => initialRoadBindNodePath;
        [SerializeField]private string initialRoadBindNodePath;

        public Vector2 SugarStackPosition => sugarStackPosition;
        [SerializeField] private Vector2 sugarStackPosition;
        
        public string SugarStackNodeName => sugarStackNodeName;
        [SerializeField] private string sugarStackNodeName;
        
        public string SugarStackBindNodePath => sugarStackBindNodePath;
        [SerializeField] private string sugarStackBindNodePath;

        public int BuilderAntCount => builderAntCount;
        [SerializeField] private int builderAntCount = 1;

        public int ErgateAntCount => ergateAntCount;
        [SerializeField] private int ergateAntCount = 2;
        
        public int UnemployedAntCount => unemployedAntCount;
        [SerializeField] private int unemployedAntCount = 2;

        public int StoredFood => storedFood;
        [SerializeField] private int storedFood;
    }
}