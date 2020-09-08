using System;
using System.Linq;
using AntDiary.Scripts.Ants;
using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// 建築中のElementに移動中のStrategy
	/// </summary>
	public class MoveStrategy : Strategy<BuilderAntData>{
		private NestPathNode _distNode = default;
		private NestBuildableElement _hostElem = default;
		
		public MoveStrategy(NestBuildableElement host, NestPathNode distNode) : base(){
			_distNode = distNode;
			_hostElem = host;
		}

		public override void StartStrategy(StrategyController<BuilderAntData> controller){
			base.StartStrategy(controller);
			
			Debug.Log($"<MoveStrategy> start ant: {Controller.Ant.transform.position.ToString()} distNode:{_distNode.WorldPosition.ToString()} distHost:{_distNode.Host.transform.position}");
			
			Controller.Ant.StartForPathNode(_distNode, HandleArrived, HandleAborted);
		}

		private void HandleArrived(){
			Debug.Log("Arrived");
			
			
			Controller.ChangeStrategy(new BuildStrategy(_hostElem));
		}
		
		private void HandleAborted(){
			Debug.Log("Aborted");
			Controller.Ant.CancelMovement();
			Controller.ChangeStrategy(new RoundStrategy());
		}

		
		public override void PeriodicUpdate(){
		}


		public override void FinishStrategy(){
			Controller.Ant.CancelMovement();
		}
	}
}