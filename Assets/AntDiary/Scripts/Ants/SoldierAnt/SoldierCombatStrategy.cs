

using UnityEngine;

namespace AntDiary.Scripts.Ants.SoldierAnt{
	public class SoldierCombatStrategy: Strategy<SoldierAntData>{
		private EnemyAnt _enemyAnt;

		public SoldierCombatStrategy(EnemyAnt enemyAnt){
			_enemyAnt = enemyAnt;

		}

		public override void StartStrategy(StrategyController<SoldierAntData> controller){
			base.StartStrategy(controller);
			
			//攻撃間隔
			UpdateInterval = AntData.CombatInterval;
		}

		public override void PeriodicUpdate(){
			Combat();
			
			//戦闘距離外なら戦闘に移行
			if(Vector3.Distance(_enemyAnt.transform.position, Controller.Ant.transform.position) > AntData.CombatDistance){
				Controller.ChangeStrategy(new SoldierChaseStrategy(_enemyAnt));
				return;
			}
			
			//倒した
			if(!_enemyAnt.Data.IsAlive){
				Controller.ChangeStrategy(new SoldierStandbyStrategy());
			}
		}

		protected void Combat(){
			throw new System.NotImplementedException();
		}

		public override void FinishStrategy(){
			
		}
	}
}