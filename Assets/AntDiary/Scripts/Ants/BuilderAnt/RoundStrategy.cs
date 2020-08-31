using System.Linq;
using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// 建築中のElementがなく、待機してるときのStrategy
	/// </summary>
	public class RoundStrategy: BuilderStrategy{
		public RoundStrategy(BuilderAnt ant) : base(ant){
			UpdateInterval = 1.0f;
		}

		public override void StartStrategy(){
			
		}

		public override void PeriodicUpdate(){
			
			var buildingElements = NestSystem.Instance.BuildingElements;

			Debug.Log("<RoundStrategy> PeriodicUpdate building?: " + buildingElements.Any());
			if(buildingElements.Any()){
				
				//これ到達可能かどうかのチェックもいる？
				var dist = buildingElements.Aggregate(
					(result, next) => 
						Vector3.Distance(result.transform.position, HostAnt.transform.position)
						< Vector3.Distance(next.transform.position, HostAnt.transform.position)
							? result : next);
				
				HostAnt.ChangeStrategy(new MoveStrategy(HostAnt, dist));
			}
			
		}

		public override void FinishStrategy(){
			
		}
	}
}