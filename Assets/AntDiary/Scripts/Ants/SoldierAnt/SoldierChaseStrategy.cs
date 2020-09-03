using UnityEngine;

namespace AntDiary.Scripts.Ants.SoldierAnt{
	/// <summary>
	/// 敵アリを追いかけるStrategy
	/// </summary>
	public class SoldierChaseStrategy: Strategy<SoldierAntData>{
		private EnemyAnt _enemyAnt;
		private NestPathNode _currentTargetNode;

		
		
		public SoldierChaseStrategy(EnemyAnt enemyAnt){
			_enemyAnt = enemyAnt;
			UpdateInterval = 0.5f;
		}

		public override void PeriodicUpdate(){
			if(!_enemyAnt.Data.IsAlive){
				Controller.ChangeStrategy(new SoldierStandbyStrategy());
			}

			//戦闘距離内なら戦闘に移行
			if(Vector3.Distance(_enemyAnt.transform.position, Controller.Ant.transform.position) < AntData.CombatDistance){
				Controller.ChangeStrategy(new SoldierCombatStrategy(_enemyAnt));
				return;
			}
			
			
			var enemyNode = _enemyAnt.SupposeCurrentPosNode();
			//敵アリの位置が変化していなければそのまま移動を続ける
			if(_currentTargetNode == enemyNode){
				return;
			}

			_currentTargetNode = enemyNode;

			
			void HandleArrived(){
				
			}

			void HandleAborted(){
				Controller.ChangeStrategy(new SoldierStandbyStrategy());
			}
			
			Controller.Ant.StartForPathNode(_currentTargetNode, HandleArrived, HandleAborted);
		}

		public override void FinishStrategy(){
			Controller.Ant.CancelMovement();
		}
	}
}