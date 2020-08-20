using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// NestElement内部の経路ノード同士の接続を示す。
    /// </summary>
    public class NestPathLocalEdge : NestPathEdge
    {
        public override bool CanGetThrough => true;
        public override NestPathNode A { get; }
        public override NestPathNode B { get; }
        
        public NestPathLocalEdge(NestPathNode a, NestPathNode b)
        {
            if(a.Host != b.Host) throw new ArgumentException("異なるNestElementに含まれるNestPathNodeどうしを接続することはできません。");
            A = a;
            B = b;
            
            RegisterToNode();
        }
    }
}