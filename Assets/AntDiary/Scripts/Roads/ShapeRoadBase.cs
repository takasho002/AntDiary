using System;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public abstract class ShapeRoadBase<T> : Road<T> where T : RoadData{
		public override bool HasPathNode => true;
		
		/// <summary>
		/// 建築にかかるコスト
		/// </summary>
		[SerializeField] protected float constructCost = 15;
        
		/// <summary>
		/// おけるかどうかの判定に使うCollider
		/// </summary>
		[SerializeField] protected Collider2D blockingShape;
		
		private readonly Dictionary<string, NestPathNode> _nodeDic = new Dictionary<string, NestPathNode>();
		private readonly List<NestPathLocalEdge> _edgeList = new List<NestPathLocalEdge>();
		
		protected abstract override void OnInitialized();
		
		/// <summary>
		/// このRoadが持つLocalNodeに追加
		/// </summary>
		/// <param name="localName"></param>
		/// <param name="exposedName"></param>
		/// <param name="localPos"></param>
		protected void AddLocalNode(string localName, string exposedName, Vector2 localPos){
			_nodeDic.Add(localName, new NestPathRoadNode(this, localPos, exposedName));
		}
		
		/// <summary>
		/// 登録されているLocalNode同士を接続
		/// </summary>
		/// <param name="localA"></param>
		/// <param name="localB"></param>
		protected void ConnectLocalNode(string localA, string localB){
			var nodeA = _nodeDic[localA] ?? throw new ArgumentException($"登録されていないLocalNameのNodeです: {localA}");
			var nodeB = _nodeDic[localB] ?? throw new ArgumentException($"登録されていないLocalNameのNodeです: {localB}");
			
			_edgeList.Add(new NestPathLocalEdge(nodeA, nodeB));
		}

		/// <summary>
		/// それぞれのLocalNodeをintersectionLocalに接続
		/// </summary>
		/// <param name="intersectionLocal"></param>
		/// <param name="otherLocal"></param>
		protected void ConnectLocalNodeToIntersection(string intersectionLocal, IEnumerable<string> otherLocal){
			foreach(var local in otherLocal){
				ConnectLocalNode(intersectionLocal, local);
			}
		}

		public override IEnumerable<NestPathNode> GetNodes(){
			return _nodeDic.Values;
		}

		public override IEnumerable<NestPathLocalEdge> GetLocalEdges(){
			return _edgeList;
		}
		
		public override Collider2D GetBlockingShape(){
			return blockingShape;
		}

		public override float RequiredResources => constructCost;
		
		
		
		#region Debug
		private void OnDrawGizmos(){
			RoadGizmosUtil.DrawNodeWithEdge(GetNodes(), GetLocalEdges());
			RoadGizmosUtil.DrawBuildableElement(this);
		}

		#endregion
	}
}