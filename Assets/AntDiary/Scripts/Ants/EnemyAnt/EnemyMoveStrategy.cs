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
			return NestSystemExtensionsQueenRoom.GetQueenRoom(NestSystem.Instance)?.GetNodes().FirstOrDefault();
		}

		public override void PeriodicUpdate(){
			float Distance(Ant antA, Ant antB) => Vector2.Distance(antA.transform.position, antB.transform.position);

			//最も近いアリ取得
			var nearlyAnt = NestSystem.Instance
				.SpawnedAnt
				.Aggregate((result, next) => {
					if(next is EnemyAnt){
						return result;
					}
					return DistanceToHostAnt(next.transform.position) < DistanceToHostAnt(result.transform.position) ? next : result;
				});
					

			
			if(Distance(Controller.Ant, nearlyAnt) < AntData.CombatDistance){
				Controller.ChangeStrategy(new EnemyCombatStrategy(nearlyAnt));
			}
		}

		public override void FinishStrategy(){
		}
	}
}