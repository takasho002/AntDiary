using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    public class NestPathElementEdge : NestPathEdge
    {
        public NestPathElementEdgeData Data { get; }
        public override NestPathNode A { get; }
        public override NestPathNode B { get; }

        public NestPathElementEdge(IEnumerable<NestElement> elements, NestPathElementEdgeData data)
        {
            Data = data;
            NestElement elementA = null;
            NestElement elementB = null;
            foreach (var e in elements)
            {
                if (e.Data.Guid == data.ElementGuidA)
                {
                    elementA = e;
                }
                else if (e.Data.Guid == data.ElementGuidB)
                {
                    elementB = e;
                }
            }

            if (elementA == default || elementB == default) throw new ArgumentException("指定されたNestElementが見つかりませんでした。");

            var nodeA = elementA.GetNodes().Where(n => n.IsExposed).FirstOrDefault(n => n.Name == data.NodeNameA);
            if (nodeA == default) throw new ArgumentException("指定されたNestPathNodeが見つかりませんでした。");
            var nodeB = elementB.GetNodes().Where(n => n.IsExposed).FirstOrDefault(n => n.Name == data.NodeNameB);
            if (nodeB == default) throw new ArgumentException("指定されたNestPathNodeが見つかりませんでした。");

            A = nodeA;
            B = nodeB;
        }

        public NestPathElementEdge(NestPathNode a, NestPathNode b)
        {
            if (a.Host == b.Host)
                throw new ArgumentException("同じNestElementのNestPathNodeをNestElementEdgeで接続することはできません。");
            A = a;
            B = b;
            Data = new NestPathElementEdgeData()
            {
                ElementGuidA = a.Host.Data.Guid,
                ElementGuidB = b.Host.Data.Guid,
                NodeNameA = a.Name,
                NodeNameB = b.Name
            };
        }
    }
}