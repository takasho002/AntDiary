using MessagePack;

namespace AntDiary.Scripts.Roads{
	[MessagePackObject()]
	public class LShapeRoadData: RoadData{
		
		public LShapeRoadData(EnumRoadDirection direction){
			Direction = direction;
		}
		
		//Keyは30~
		[Key(30)] public EnumRoadDirection Direction { get; set; }

	}
}