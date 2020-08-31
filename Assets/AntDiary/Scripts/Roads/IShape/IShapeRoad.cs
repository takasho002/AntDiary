using System;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class IShapeRoad: ShapeRoadBase<IShapeRoadData>{
		
		/// <summary>
		/// 端から端までの距離/2
		/// </summary>
		[SerializeField] private float radius = 2;

		protected override void OnInitialized(){

			switch(SelfData.Direction){
				case EnumRoadHVDirection.Horizontal:
					AddLocalNode("left", "wild_left", Vector2.left * radius);
					AddLocalNode("right", "wild_right", Vector2.right * radius);
					ConnectLocalNode("left", "right");
					break;
				case EnumRoadHVDirection.Vertical:
					AddLocalNode("top", "wild_top", Vector2.up * radius);
					AddLocalNode("bottom", "wild_bottom", Vector2.down * radius);
					ConnectLocalNode("top", "bottom");
					break;
			}
		}
	}
}