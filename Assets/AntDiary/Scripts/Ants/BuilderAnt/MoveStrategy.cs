using System;
using System.Linq;

namespace AntDiary{
	/// <summary>
	/// 建築中のElementに移動中のStrategy
	/// </summary>
	public class MoveStrategy : BuilderStrategy{
		private NestBuildableElement _distElement;
		
		public MoveStrategy(BuilderAnt ant, NestBuildableElement distElement) : base(ant){
			_distElement = distElement;
		}

		public override void StartStrategy(){
			var distPathNode = (NestPathNode) _distElement.GetBuildingNode().First();
			
			HostAnt.StartForPathNode(distPathNode, HandleArrived, HandleAborted);
		}

		private void HandleArrived(){
			HostAnt.ChangeStrategy(new BuildStrategy(HostAnt, _distElement));
		}
		
		private void HandleAborted(){
			HostAnt.CancelMovement();
			HostAnt.ChangeStrategy(new RoundStrategy(HostAnt));
		}

		
		public override void PeriodicUpdate(){
		}


		public override void FinishStrategy(){
			HostAnt.CancelMovement();
		}
	}
}