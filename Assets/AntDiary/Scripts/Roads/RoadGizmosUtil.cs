using UnityEngine;

namespace AntDiary.Scripts.Roads{
	/// <summary>
	/// RoadでGizmoを描画する用のユーティリティ
	/// </summary>
	public class RoadGizmosUtil{
		/// <summary>
		/// Nodeを描画する円のフレーム色
		/// </summary>
		public static Color NodeColor = Color.cyan;
		
		
		/// <summary>
		/// Edgeを描画する線のフレーム色
		/// </summary>
		public static Color EdgeColor = Color.blue;
		
		/// <summary>
		/// Nodeを描画するか
		/// </summary>
		public static bool DrawNode = true;
		
		/// <summary>
		/// Edgeを描画するか
		/// </summary>
		public static bool DrawEdge = true;
		
		public static void DrawNodeWithEdge(NestPathNode[] nodes, NestPathLocalEdge[] edges){
			var prevColor = Gizmos.color;

			if(DrawNode){
				Gizmos.color = NodeColor;
				foreach(var node in nodes){
					Gizmos.DrawWireSphere(node.WorldPosition, 0.1f);
				}
			}

			if(DrawEdge){
				Gizmos.color = EdgeColor;
				foreach(var edge in edges){
					Gizmos.DrawLine(edge.A.WorldPosition, edge.B.WorldPosition);
				}
			}
			
			
		}
	}
}