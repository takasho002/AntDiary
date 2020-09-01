using AntDiary.Scripts.Ants;
using AntDiary.Scripts.Ants.SoldierAnt;

namespace AntDiary{
	public class SoldierAnt: StrategyAnt<SoldierAntData>{

		protected override float MovementSpeed => 1.2f;
		protected override Strategy<SoldierAntData> CreateInitialStrategy(){
			return new SoldierStandbyStrategy();
		}
	}
}