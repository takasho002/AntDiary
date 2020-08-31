using System;
using System.Linq;
using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// 建築中のElementに移動中のStrategy
	/// </summary>
	public class MoveStrategy : BuilderStrategy{
		private NestPathNode _distNode;
		private NestBuildableElement _hostElem;
		
		public MoveStrategy(BuilderAnt ant, NestBuildableElement host, NestPathNode distNode) : base(ant){
			_distNode = distNode;
			_hostElem = host;
		}

		public override void StartStrategy(){
			
			
			Debug.Log($"<MoveStrategy> start ant: {HostAnt.transform.position.ToString()} distNode:{_distNode.WorldPosition.ToString()} distHost:{_distNode.Host.transform.position}");
			
			HostAnt.StartForPathNode(_distNode, HandleArrived, HandleAborted);
		}

		private void HandleArrived(){
			Debug.Log("Arrived");
			
			
			HostAnt.ChangeStrategy(new BuildStrategy(HostAnt, _hostElem));
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