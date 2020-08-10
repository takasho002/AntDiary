namespace AntDiary{
	public class PathParam{
		public PathParam(float length, float costCoefficient){
			Length = length;
			CostCoefficient = costCoefficient;
		}


		internal float Length{ get; }
		internal float CostCoefficient{ get; }

		internal float GetCost => Length * CostCoefficient;
	}
}