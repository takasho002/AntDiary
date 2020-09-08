using AntDiary.Scripts.Ants;

namespace AntDiary{
	
	public class EnemyAnt: StrategyAnt<EnemyAntData>{
		
		

		protected override float MovementSpeed => SelfCommonData.BasicMovementSpeed;
		
		protected override Strategy<EnemyAntData> CreateInitialStrategy(){
			return new EnemyMoveStrategy();
			
		}
	}
}