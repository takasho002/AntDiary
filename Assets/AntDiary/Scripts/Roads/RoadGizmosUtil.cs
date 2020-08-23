using System.Linq;
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
		/// 接続されているNodeを描画する円のフレーム色
		/// </summary>
		public static Color ConnectedNodeColor = Color.green;
		
		
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
			// var prevColor = Gizmos.color;

			if(DrawNode){
				
				foreach(var node in nodes){
					
					Gizmos.color = node.Edges.OfType<NestPathElementEdge>().Any() ? Color.blue : Color.green;
					
					Gizmos.DrawWireSphere(node.WorldPosition, 0.15f);
				}
			}
			
			if(DrawEdge){
				
				foreach(var edge in edges){
					// Debug.Log("Color");
					Gizmos.color = Color.red;
					// Debug.Log("Draw");
					Gizmos.DrawLine(edge.A.WorldPosition, edge.B.WorldPosition);
				}
			}

			// Gizmos.color = prevColor;

		}
	}
}