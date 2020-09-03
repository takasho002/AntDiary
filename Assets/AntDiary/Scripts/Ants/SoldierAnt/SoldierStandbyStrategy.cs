
using System.Linq;
using AntDiary.Scripts.Ants;
using AntDiary.Scripts.Ants.SoldierAnt;

namespace AntDiary{
	public class SoldierStandbyStrategy: Strategy<SoldierAntData>{
		
		public SoldierStandbyStrategy(): base(){
			UpdateInterval = 1.0f;
		}
		
		public override void StartStrategy(StrategyController<SoldierAntData> controller){
			base.StartStrategy(controller);
			
			//待機状態に戻ってきたときにすぐ応答する用
			PeriodicUpdate();
		}

		public override void PeriodicUpdate(){
			EnemyAnt enemyAnt = GetTargetAnt();
			
			if(enemyAnt == null) return;
			
			Controller.ChangeStrategy(new SoldierChaseStrategy(enemyAnt));
		}

		protected EnemyAnt GetTargetAnt(){
			var enemyList = NestSystem.Instance.SpawnedAnt.OfType<EnemyAnt>();
			return enemyList.First();
		}

		
		public override void FinishStrategy(){
			
		}
	}
}