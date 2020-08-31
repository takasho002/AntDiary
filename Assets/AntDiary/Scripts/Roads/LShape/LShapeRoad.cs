using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class LShapeRoad: ShapeRoadBase<LShapeRoadData>{
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;
		
		
		protected override void OnInitialized(){
			
			switch(SelfData.Direction){
				case EnumRoadDirection.Top:
					AddLocalNode("top", "wild_top", Vector2.up * radius);
					AddLocalNode("right", "wild_right", Vector2.right * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"top", "right"});
					// _nodes = new[]{
					// 	topNode, centerNode, rightNode
					// };
					break;
				case EnumRoadDirection.Right:
					AddLocalNode("right", "wild_right", Vector2.right * radius);
					AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"right", "bottom"});
					// _nodes = new[]{
					// 	rightNode, centerNode, bottomNode
					// };
					break;
				case EnumRoadDirection.Bottom:
					AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
					AddLocalNode("left", "wild_left", Vector2.left * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"bottom", "left"});
					// _nodes = new[]{
					// 	bottomNode, centerNode, leftNode
					// };
					break;
				case EnumRoadDirection.Left:
					AddLocalNode("left", "wild_left", Vector2.left * radius);
					AddLocalNode("top", "wild_top", Vector2.up * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"left", "top"});
					// _nodes = new[]{
					// 	leftNode, centerNode, topNode
					// };
					break;
			}
			
		}
		
		
	}
}