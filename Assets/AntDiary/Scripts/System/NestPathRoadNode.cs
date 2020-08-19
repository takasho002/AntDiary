using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// 道用のNestPathNode
    /// </summary>
    public class NestPathRoadNode : NestPathNode
    {
        public NestPathRoadNode(NestElement host, Vector2 localPosition, string name = "") : base(host, localPosition, name)
        {
        }
        
        public override bool IsConnectable(NestPathNode other)
        {
            if (other is NestPathRoadNode || other is NestPathRoomNode)
            {
                if (this.Name == "wild" || other.Name == "wild") return true;
                switch (other.Name)
                {
                    case "right":
                        return this.Name == "left";
                    case "top":
                        return this.Name == "bottom";
                    case "left":
                        return this.Name == "right";
                    case "bottom":
                        return this.Name == "top";
                    default:
                        return true;
                }
            }
            return true;
        }
    }
}