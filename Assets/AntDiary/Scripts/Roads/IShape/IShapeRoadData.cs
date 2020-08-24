using MessagePack;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	[MessagePackObject()]
	
	public class IShapeRoadData: RoadData{

		public IShapeRoadData(EnumRoadHVDirection direction){
			Direction = direction;
		}
		
		//Keyは30~
		[Key(30)] public EnumRoadHVDirection Direction { get; set; }
		
	}
}