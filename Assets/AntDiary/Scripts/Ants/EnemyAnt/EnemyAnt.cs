using AntDiary.Scripts.Ants;

namespace AntDiary{
	
	public class EnemyAnt: StrategyAnt<EnemyAntData>{

		protected override float MovementSpeed => 1.2f;
		
		protected override Strategy<EnemyAntData> CreateInitialStrategy(){
			throw new System.NotImplementedException();
		}
	}
}