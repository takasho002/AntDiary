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
		public static Color NodeColor = Color.green;
		
		/// <summary>
		/// 接続されているNodeを描画する円のフレーム色
		/// </summary>
		public static Color ConnectedNodeColor = Color.blue;
		
		
		/// <summary>
		/// Edgeを描画する線のフレーム色
		/// </summary>
		public static Color EdgeColor = Color.red;
		
		/// <summary>
		/// Nodeを描画するか
		/// </summary>
		public static bool DrawNode = true;
		
		/// <summary>
		/// Edgeを描画するか
		/// </summary>
		public static bool DrawEdge = true;

		/// <summary>
		/// BuildableElementへの網掛けを表示するか
		/// </summary>
		public static bool DrawElement = true;
		
		/// <summary>
		/// 建造中のElementの網掛け色
		/// </summary>
		public static Color ElementColor = new Color(1.0f, 0.65f, 0.0f, 0.3f);
		
		/// <summary>
		/// 建造が終わったElementの網掛け色
		/// </summary>
		public static Color BuiltElementColor = new Color(0.0f, 0.75f, 1.0f, 0.3f);

		
		public static void DrawNodeWithEdge(NestPathNode[] nodes, NestPathLocalEdge[] edges){
			// var prevColor = Gizmos.color;

			if(DrawNode && nodes != null){
				
				foreach(var node in nodes){
					
					Gizmos.color = node.Edges.OfType<NestPathElementEdge>().Any() ? ConnectedNodeColor : NodeColor;
					
					Gizmos.DrawWireSphere(node.WorldPosition, 0.15f);
				}
			}
			
			if(DrawEdge && edges != null){
				
				foreach(var edge in edges){
					// Debug.Log("Color");
					Gizmos.color = EdgeColor;
					// Debug.Log("Draw");
					Gizmos.DrawLine(edge.A.WorldPosition, edge.B.WorldPosition);
				}
			}

			// Gizmos.color = prevColor;

		}

		public static void DrawBuildableElement<T>(NestBuildableElement<T> element) where T: NestBuildableElementData{
			if(element == null || element.Data == null) return;
			
			var box = element.GetBlockingShape() as BoxCollider2D;
			if(box == null) return;
			
			Gizmos.color = element.IsUnderConstruction ? ElementColor : BuiltElementColor;
			Gizmos.DrawCube(box.transform.position, box.size*2);
		}
	}
}