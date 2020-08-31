using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class TShapeRoad: ShapeRoadBase<TShapeRoadData>{
		
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;
		
		
		protected override void OnInitialized(){
			
			switch(SelfData.Direction){
				
				case EnumRoadDirection.Top:
					AddLocalNode("left", "wild_left", Vector2.left * radius);
					AddLocalNode("top", "wild_top", Vector2.up * radius);
					AddLocalNode("right", "wild_right", Vector2.right * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"left", "top", "right"});
					
					// 	_nodes = new[]{
					// 		leftNode, topNode, rightNode, centerNode
					// 	};
					break;
				
				case EnumRoadDirection.Right:
					AddLocalNode("top", "wild_top", Vector2.up * radius);
					AddLocalNode("right", "wild_right", Vector2.right * radius);
					AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"top", "right", "bottom"});
					// 	_nodes = new[]{
					// 		rightNode, bottomNode, leftNode, centerNode
					// 	};
					break;
				case EnumRoadDirection.Bottom:
					AddLocalNode("right", "wild_right", Vector2.right * radius);
					AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
					AddLocalNode("left", "wild_left", Vector2.left * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"right", "bottom", "left"});
					// 	_nodes = new[]{
					// 		rightNode, bottomNode, leftNode, centerNode
					// 	};
					break;
				case EnumRoadDirection.Left:
					AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
					AddLocalNode("left", "wild_left", Vector2.left * radius);
					AddLocalNode("top", "wild_top", Vector2.up * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"bottom", "left", "top"});
					// 	_nodes = new[]{
					// 		bottomNode, leftNode, topNode, centerNode
					// 	};
					break;
				
			};
			
		}
		
	}
}