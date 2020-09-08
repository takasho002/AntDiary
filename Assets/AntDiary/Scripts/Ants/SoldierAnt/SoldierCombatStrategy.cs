

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

			UpdateInterval = GetAntCommonData<SoldierAntCommonData>().CombatInterval;
		}

		public override void PeriodicUpdate(){
			//戦闘距離外なら追いかけに移行
			var combatDistance = GetAntCommonData<SoldierAntCommonData>().CombatDistance;

			if(Vector3.Distance(_enemyAnt.transform.position, Controller.Ant.transform.position) > combatDistance){
				Controller.ChangeStrategy(new SoldierChaseStrategy(_enemyAnt));
				return;
			}
			
			//倒した
			if(!_enemyAnt.Data.IsAlive){
				Controller.ChangeStrategy(new SoldierStandbyStrategy());
			}
			
			CombatUtil.AttackToAnt(Controller.Ant, _enemyAnt);
		}

		

		public override void FinishStrategy(){
			
		}
	}
}