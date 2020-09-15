using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class CrossShapeRoad: ShapeRoadBase<CrossShapeRoadData>{
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;
		
		protected override void OnInitialized(){
			if (!IsUnderConstruction) SetImage(EnumNestImage.Built);

			AddLocalNode("top", "top", Vector2.up * radius);
			AddLocalNode("right", "right", Vector2.right * radius);
			AddLocalNode("bottom", "bottom", Vector2.down * radius);
			AddLocalNode("left", "left", Vector2.left * radius);
			AddLocalNode("center", "", Vector2.zero * radius);
			
			ConnectLocalNodeToIntersection("center", new []{"top", "right", "bottom", "left"});
			
		}
		
	}
}