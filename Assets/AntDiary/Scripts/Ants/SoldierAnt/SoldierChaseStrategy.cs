using UnityEngine;

namespace AntDiary.Scripts.Ants.SoldierAnt{
	public class SoldierChaseStrategy: Strategy<SoldierAntData>{

		private EnemyAnt _enemyAnt;
		private NestPathNode _currentTargetNode;

		/// <summary>
		/// 戦闘に移行する距離
		/// </summary>
		private float _combatDistance = 0.5f;
		
		public SoldierChaseStrategy(EnemyAnt enemyAnt){
			_enemyAnt = enemyAnt;
			UpdateInterval = 0.5f;
		}

		public override void PeriodicUpdate(){
			if(!_enemyAnt.Data.IsAlive){
				Controller.ChangeStrategy(new SoldierStandbyStrategy());
			}

			//戦闘距離内なら戦闘に移行
			if(Vector3.Distance(_enemyAnt.transform.position, Controller.Ant.transform.position) < _combatDistance){
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