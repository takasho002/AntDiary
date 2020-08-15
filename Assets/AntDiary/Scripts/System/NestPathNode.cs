using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// NestElementsにおけるパスのノードを示す。
    /// </summary>
    public abstract class NestPathNode
    {
        public NestElement Host { get; }
        
        public Vector2 LocalPosition { get; }

        public Vector2 WorldPosition => (Vector2)Host.transform.position + LocalPosition;
        
        /// <summary>
        /// 接続時に使用する識別用のID。
        /// </summary>
        public string Name { get; }
        public bool IsExposed { get; }

        public NestPathNode(NestElement host, Vector2 localPosition, string name = "")
        {
            Host = host;
            LocalPosition = localPosition;
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
                IsExposed = true;
            }
        }

        /// <summary>
        /// あるNestPathNodeとElementを越えた接続が可能であるか調べる。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool IsConnectable(NestPathNode other);
    }
}