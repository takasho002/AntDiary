using System;
using System.Linq;
using UnityEngine;

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
			Debug.Log($"<MoveStrategy> start ant: {HostAnt.transform.position.ToString()} dist:${_distElement.transform.position.ToString()}");

			
			var distPathNode = (NestPathNode) _distElement.GetBuildingNode().First();
			
			HostAnt.StartForPathNode(distPathNode, HandleArrived, HandleAborted);
		}

		private void HandleArrived(){
			Debug.Log("Arrived");
			HostAnt.ChangeStrategy(new BuildStrategy(HostAnt, _distElement));
		}
		
		private void HandleAborted(){
			Debug.Log("Aborted");
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