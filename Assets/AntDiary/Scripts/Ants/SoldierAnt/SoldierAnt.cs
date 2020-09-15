using AntDiary.Scripts.Ants;
using AntDiary.Scripts.Ants.SoldierAnt;

namespace AntDiary{
	public class SoldierAnt: StrategyAnt<SoldierAntData>{

		protected override float MovementSpeed => CommonData.BasicMovementSpeed;
		protected override Strategy<SoldierAntData> CreateInitialStrategy(){
			return new SoldierStandbyStrategy();
		}
	}
}