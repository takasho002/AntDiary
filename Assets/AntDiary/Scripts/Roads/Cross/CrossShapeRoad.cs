using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class CrossShapeRoad: Road<CrossShapeRoadData>{
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;
		
		
		private NestPathNode[] _nodes;
		private NestPathLocalEdge[] _edges;
		
		
		protected override void OnInitialized(){
			var topNode = new NestPathRoadNode(this, Vector2.up * radius, "wild_top");
			var rightNode = new NestPathRoadNode(this, Vector2.right * radius, "wild_right");
			var bottomNode = new NestPathRoadNode(this, Vector2.down * radius, "wild_bottom");
			var leftNode = new NestPathRoadNode(this, Vector2.left * radius, "wild_left");
			var centerNode = new NestPathRoadNode(this, Vector2.zero, "");
			
			_nodes = new[]{
				topNode, rightNode, bottomNode, leftNode, centerNode
			};
			
			_edges = new[]{
				new NestPathLocalEdge(_nodes[0], _nodes[4]),
				new NestPathLocalEdge(_nodes[1], _nodes[4]),
				new NestPathLocalEdge(_nodes[2], _nodes[4]),
				new NestPathLocalEdge(_nodes[3], _nodes[4]),
			};
		}
		
		
		public override bool HasPathNode => true;
		
		public override IEnumerable<NestPathNode> GetNodes(){
			return _nodes;
		}

		public override IEnumerable<NestPathLocalEdge> GetLocalEdges(){
			return _edges;
		}

		#region Debug
		private void OnDrawGizmos(){
			RoadGizmosUtil.DrawNodeWithEdge(_nodes, _edges);
		}

		#endregion
	}
}