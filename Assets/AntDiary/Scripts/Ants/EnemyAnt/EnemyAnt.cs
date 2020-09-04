using AntDiary.Scripts.Ants;

namespace AntDiary{
	
	public class EnemyAnt: StrategyAnt<EnemyAntData>{
		
		

		protected override float MovementSpeed => SelfData.MovementSpeed;
		
		protected override Strategy<EnemyAntData> CreateInitialStrategy(){
			return new EnemyMoveStrategy();
			
			//わからんポイント
			//女王アリの部屋の取得方法
			//攻撃処理
			//攻撃モード移行の判定
			//攻撃速度とかのパラメータをどうやって持つか
			//
		}
	}
}