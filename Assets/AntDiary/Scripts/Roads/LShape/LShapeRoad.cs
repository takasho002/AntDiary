using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class LShapeRoad: ShapeRoadBase<LShapeRoadData>{
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;
		
		
		protected override void OnInitialized(){
			if (!IsUnderConstruction) SetImage(EnumNestImage.Built);
			switch (SelfData.Direction){
				case EnumRoadDirection.Top:
					AddLocalNode("top", "top", Vector2.up * radius);
					AddLocalNode("right", "right", Vector2.right * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"top", "right"});
					// _nodes = new[]{
					// 	topNode, centerNode, rightNode
					// };
					break;
				case EnumRoadDirection.Right:
					AddLocalNode("right", "right", Vector2.right * radius);
					AddLocalNode("bottom", "bottom", Vector2.down * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"right", "bottom"});
					// _nodes = new[]{
					// 	rightNode, centerNode, bottomNode
					// };
					break;
				case EnumRoadDirection.Bottom:
					AddLocalNode("bottom", "bottom", Vector2.down * radius);
					AddLocalNode("left", "left", Vector2.left * radius);
					AddLocalNode("center", "", Vector2.zero * radius);
					ConnectLocalNodeToIntersection("center", new []{"bottom", "left"});
					// _nodes = new[]{
					// 	bottomNode, centerNode, leftNode
					// };
					break;
				case EnumRoadDirection.Left:
					AddLocalNode("left", "left", Vector2.left * radius);
					AddLocalNode("top", "top", Vector2.up * radius);
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