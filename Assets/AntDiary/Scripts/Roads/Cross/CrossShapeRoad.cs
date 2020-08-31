using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class CrossShapeRoad: ShapeRoadBase<CrossShapeRoadData>{
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;
		
		protected override void OnInitialized(){
			AddLocalNode("top", "wild_top", Vector2.up * radius);
			AddLocalNode("right", "wild_right", Vector2.right * radius);
			AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
			AddLocalNode("left", "wild_left", Vector2.left * radius);
			AddLocalNode("center", "", Vector2.zero * radius);
			
			ConnectLocalNodeToIntersection("center", new []{"top", "right", "bottom", "left"});
			
		}
		
	}
}