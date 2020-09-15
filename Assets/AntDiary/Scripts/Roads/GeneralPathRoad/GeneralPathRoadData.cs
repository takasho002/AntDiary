using System;
using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    [MessagePackObject(), Serializable]
    public class GeneralPathRoadData : NestElementData
    {
        [SerializeField] private string name;
        [SerializeField] private Rect blockingShape;
        [SerializeField] private List<GeneralPathRoadNodeData> nodeData = new List<GeneralPathRoadNodeData>();
        [SerializeField] private List<GeneralPathRoadEdgeData> edgeData = new List<GeneralPathRoadEdgeData>();

        /// <summary>
        /// シーン上にGeneralPathRoadDataが複数存在する場合に、インスタンスを一意に定めるためのID。とくに重複チェックなどの機能は持たない
        /// </summary>
        [Key(30)]
        public string Name
        {
            get => name;
            set => name = value;
        }
        
        [Key(31)]
        public Rect BlockingShape
        {
            get => blockingShape;
            set => blockingShape = value;
        }

        [Key(32)]
        public List<GeneralPathRoadNodeData> NodeData
        {
            get => nodeData;
            set => nodeData = value;
        }

        [Key(33)]
        public List<GeneralPathRoadEdgeData> EdgeData
        {
            get => edgeData;
            set => edgeData = value;
        }
    }

    [MessagePackObject(), Serializable]
    public class GeneralPathRoadNodeData
    {
        [SerializeField] private string name;
        [SerializeField] private Vector2 position;

        [Key(0)]
        public string Name
        {
            get => name;
            set => name = value;
        }

        [Key(1)]
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
    }
    
    [MessagePackObject(), Serializable]
    public class GeneralPathRoadEdgeData
    {
        [SerializeField] private int indexA;
        [SerializeField] private int indexB;

        [Key(0)]
        public int IndexA
        {
            get => indexA;
            set => indexA = value;
        }

        [Key(1)]
        public int IndexB
        {
            get => indexB;
            set => indexB = value;
        }
    }
}