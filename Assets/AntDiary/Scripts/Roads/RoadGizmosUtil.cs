using System.Collections.Generic;
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

		public static Color NonPassingEdgeColor = new Color(0.55f, 0.00f, 0.90f);
		
		/// <summary>
		/// 建造中のElementの網掛け色
		/// </summary>
		public static Color ElementColor = new Color(1.0f, 0.65f, 0.0f, 0.3f);
		
		/// <summary>
		/// 建造が終わったElementの網掛け色
		/// </summary>
		public static Color BuiltElementColor = new Color(0.0f, 0.75f, 1.0f, 0.3f);

		
		/// <summary>
		/// Nodeを描画するか
		/// </summary>
		public static bool ShowNode = true;
		
		/// <summary>
		/// Edgeを描画するか
		/// </summary>
		public static bool ShowEdge = true;

		/// <summary>
		/// BuildableElementへの網掛けを表示するか
		/// </summary>
		public static bool ShowElement = true;
		
		
		public static void DrawNode(IEnumerable<NestPathNode> nodes){
			if(!ShowNode || nodes == null) return;
			
			foreach(var node in nodes){
				Gizmos.color = node.Edges.OfType<NestPathElementEdge>().Any() ? ConnectedNodeColor : NodeColor;
					
				Gizmos.DrawWireSphere(node.WorldPosition, 0.15f);
			}
		}

		public static void DrawEdge(IEnumerable<IPathEdge> edges){
			if(!ShowEdge || edges == null) return;
			foreach(var edge in edges){
					
				// Debug.Log("Color");
				Gizmos.color = edge.CanGetThrough ? EdgeColor : NonPassingEdgeColor;
				// Debug.Log("Draw");
				Gizmos.DrawLine(edge.A.WorldPosition, edge.B.WorldPosition);
			}
		}
		
		public static void DrawNodeWithEdge(IEnumerable<NestPathNode> nodes, IEnumerable<IPathEdge> edges){
			DrawNode(nodes);
			DrawEdge(edges);
		}

		public static void DrawBuildableElement(NestBuildableElement element){
			if(!ShowElement) return;
			if(element == null || element.Data == null) return;
			
			var box = element.GetBlockingShape() as BoxCollider2D;
			if(box == null) return;
			
			Gizmos.color = element.IsUnderConstruction ? ElementColor : BuiltElementColor;
			Gizmos.DrawCube(box.transform.position, box.size*2);
		}
	}
}