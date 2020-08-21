using System;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class IShapeRoad: Road<IShapeRoadData>{
		[SerializeField] private Collider2D blockingShape;
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 1;
		
		private NestPathNode[] _nodes;
		private NestPathLocalEdge[] _edges;
		
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

		protected override void OnInitialized(){

			switch(SelfData.Direction){
				case EnumRoadHVDirection.Horizontal:
					_nodes = new[]{
						new NestPathRoadNode(this, Vector2.left * radius, "wild_left"),
						new NestPathRoadNode(this, Vector2.right * radius, "wild_right")
					};
					break;
				case EnumRoadHVDirection.Vertical:
					_nodes = new[]{
						new NestPathRoadNode(this, Vector2.up * radius, "wild_top"),
						new NestPathRoadNode(this, Vector2.down * radius, "wild_bottom")
					};
					break;
			};
			
			_edges = new[]{
				new NestPathLocalEdge(_nodes[0], _nodes[1])
			};
		}

		public override float RequiredResources{ get; }
		
		//TODO ギズモを追加

		#region Debug

		private void OnDrawGizmos(){
			
		}

		#endregion
	}
}