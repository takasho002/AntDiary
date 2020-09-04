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
					AddLocalNode("left", "left", Vector2.left * radius);
					AddLocalNode("right", "right", Vector2.right * radius);
					ConnectLocalNode("left", "right");
					break;
				case EnumRoadHVDirection.Vertical:
					AddLocalNode("top", "top", Vector2.up * radius);
					AddLocalNode("bottom", "bottom", Vector2.down * radius);
					ConnectLocalNode("top", "bottom");
					break;
			}
		}
	}
}