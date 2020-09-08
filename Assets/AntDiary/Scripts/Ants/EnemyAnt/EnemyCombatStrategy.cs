using AntDiary.Scripts.Ants;
using UnityEngine;

namespace AntDiary{
	public class EnemyCombatStrategy: Strategy<EnemyAntData>{
		private Ant _targetAnt;
		
		public EnemyCombatStrategy(Ant target){
			_targetAnt = target;
		}

		public override void StartStrategy(StrategyController<EnemyAntData> controller){
			base.StartStrategy(controller);
			UpdateInterval = GetAntCommonData<EnemyAntCommonData>().CombatInterval;
			
		}

		public override void PeriodicUpdate(){
			var combatDistance = GetAntCommonData<EnemyAntCommonData>().CombatDistance;
			
			
			//戦闘距離外なら追いかけに移行
			if(Vector3.Distance(_targetAnt.transform.position, Controller.Ant.transform.position) > combatDistance){
				Controller.ChangeStrategy(new EnemyMoveStrategy());
				return;
			}
			
			//倒してた
			if(!_targetAnt.Data.IsAlive){
				Controller.ChangeStrategy(new EnemyMoveStrategy());
			}
			
			CombatUtil.AttackToAnt(Controller.Ant, _targetAnt);
		}
		
		protected void Combat(){
			throw new System.NotImplementedException();
		}

		public override void FinishStrategy(){
			
		}
	}
}