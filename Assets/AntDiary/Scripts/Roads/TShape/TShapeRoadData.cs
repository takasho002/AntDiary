using MessagePack;

namespace AntDiary.Scripts.Roads{
	[MessagePackObject()]
	public class TShapeRoadData: RoadData{
		
		public TShapeRoadData()
		{
		}
		
		public TShapeRoadData(EnumRoadDirection direction){
			Direction = direction;
		}
		
		//Keyは30~
		[Key(30)] public EnumRoadDirection Direction { get; set; }

	}
}