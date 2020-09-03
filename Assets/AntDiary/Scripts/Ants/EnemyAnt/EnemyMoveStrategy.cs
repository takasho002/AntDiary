using System.Linq;
using AntDiary.Scripts.Ants;
using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// 目的地に向かって動き続けるStrategy
	/// </summary>
	public class EnemyMoveStrategy: Strategy<EnemyAntData>{

		private NestPathNode _targetNode;
		
		public override void StartStrategy(StrategyController<EnemyAntData> controller){
			base.StartStrategy(controller);
			
			UpdateInterval = 1.0f;
			
			_targetNode = GetTargetPathNode();
			
			Controller.Ant.StartForPathNode(_targetNode, HandleArrived, HandleAborted);

		}

		private void HandleArrived(){
			
		}

		private void HandleAborted(){
			
		}
		

		/// <summary>
		/// 目的地のNestPathNode
		/// 女王アリの巣
		/// たどり着けない場合を考慮しない
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		protected NestPathNode GetTargetPathNode(){
			throw new System.NotImplementedException();
		}

		public override void PeriodicUpdate(){
			float Distance(Ant antA, Ant antB) => Vector2.Distance(antA.transform.position, antB.transform.position);

			var nearlyAnt = NestSystem.Instance
				.SpawnedAnt
				.Aggregate((result, next) =>
					Distance(next, Controller.Ant) < Distance(result, Controller.Ant) ? next : result);

			if(Distance(Controller.Ant, nearlyAnt) < AntData.CombatDistance){
				Controller.ChangeStrategy(new EnemyCombatStrategy(nearlyAnt));
			}
		}

		public override void FinishStrategy(){
		}
	}
}