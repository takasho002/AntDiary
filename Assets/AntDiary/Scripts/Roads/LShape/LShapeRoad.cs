using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class LShapeRoad: Road<LShapeRoadData>{
		[SerializeField] private Collider2D blockingShape;
		
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
			
			
			switch(SelfData.Direction){
				case EnumRoadDirection.Top:
					_nodes = new[]{
						topNode, centerNode, rightNode
					};
					break;
				case EnumRoadDirection.Right:
					_nodes = new[]{
						rightNode, centerNode, bottomNode
					};
					break;
				case EnumRoadDirection.Bottom:
					_nodes = new[]{
						bottomNode, centerNode, leftNode
					};
					break;
				case EnumRoadDirection.Left:
					_nodes = new[]{
						leftNode, centerNode, topNode
					};
					break;
			};
			
			_edges = new[]{
				new NestPathLocalEdge(_nodes[0], _nodes[1]),
				new NestPathLocalEdge(_nodes[1], _nodes[2]),
			};
		}
		
		
		public override Collider2D GetBlockingShape(){
			return blockingShape;
		}

		public override bool HasPathNode => true;
		
		public override IEnumerable<NestPathNode> GetNodes(){
			return _nodes;
		}

		public override IEnumerable<NestPathLocalEdge> GetLocalEdges(){
			return _edges;
		}

		public override float RequiredResources{ get; }
		
		
		#region Debug
		private void OnDrawGizmos(){
			RoadGizmosUtil.DrawNodeWithEdge(_nodes, _edges);
		}

		#endregion
	}
}