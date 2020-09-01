namespace AntDiary.Scripts.Ants.SoldierAnt{
	public class SoldierCombatStrategy: Strategy<SoldierAntData>{
		private EnemyAnt _enemyAnt;

		public SoldierCombatStrategy(EnemyAnt enemyAnt){
			_enemyAnt = enemyAnt;

			UpdateInterval = 1.0f;
		}

		public override void PeriodicUpdate(){
			Combat();

			//攻撃範囲内にいるかどうかの判定も多分いる
			
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