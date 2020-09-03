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
			UpdateInterval = AntData.CombatInterval;
			
		}

		public override void PeriodicUpdate(){
			Combat();
			
			//戦闘距離外なら追いかけに移行
			if(Vector3.Distance(_targetAnt.transform.position, Controller.Ant.transform.position) > AntData.CombatDistance){
				Controller.ChangeStrategy(new EnemyMoveStrategy());
				return;
			}
			
			//倒した
			if(!_targetAnt.Data.IsAlive){
				Controller.ChangeStrategy(new EnemyMoveStrategy());
			}
		}
		
		protected void Combat(){
			throw new System.NotImplementedException();
		}

		public override void FinishStrategy(){
			
		}
	}
}