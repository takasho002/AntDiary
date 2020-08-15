using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// 部屋用のNestPathNode
    /// </summary>
    public class NestPathRoomNode : NestPathNode
    {
        public NestPathRoomNode(NestElement host, Vector2 localPosition, string name = "") : base(host, localPosition, name)
        {
        }

        public override bool IsConnectable(NestPathNode other)
        {
            //道のノードとだけ接続可能
            if (other is NestPathRoadNode)
            {
                switch (this.Name)
                {
                    case "right":
                        return other.Name == "left";
                    case "top":
                        return other.Name == "bottom";
                    case "left":
                        return other.Name == "right";
                    case "bottom":
                        return other.Name == "top";
                    default:
                        return true;
                    
                }
            }

            return false;
        }
        
    }
}