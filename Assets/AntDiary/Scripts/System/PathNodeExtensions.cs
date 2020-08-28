using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    public static class PathNodeExtensions
    {
        /// <summary>
        /// このIPathNodeに接続されているIPathNodeをすべて取得する。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeSuspendedEdge">CanGetThroughがfalseなedgeによる接続を含むか。</param>
        /// <returns></returns>
        public static IEnumerable<IPathNode>
            GetConnectedNodes(this IPathNode target, bool includeSuspendedEdge = false)
        {
            return target.Edges.Where(e => includeSuspendedEdge || e.CanGetThrough).Select(e => e.GetOtherNode(target));
        }

        /// <summary>
        /// このIPathNodeに接続されているNestPathNodeのうち、別のNestElementに属しているものをすべて取得する。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeSuspendedEdge">CanGetThroughがfalseなedgeによる接続を含むか。</param>
        /// <returns></returns>
        public static IEnumerable<IPathNode> GetConnectedNodesForeign(this NestPathNode target,
            bool includeSuspendedEdge = false)
        {
            return target.Edges.Where(e => includeSuspendedEdge || e.CanGetThrough).OfType<NestPathElementEdge>()
                .Select(e => e.GetOtherNode(target));
        }

        public static IPathNode GetOtherNode(this IPathEdge target, IPathNode one)
        {
            if (target.A == one) return target.B;
            if (target.B == one) return target.A;
            throw new ArgumentException("IPathEdgeは指定されたIPathNodeを含んでいません。");
        }
    }
}