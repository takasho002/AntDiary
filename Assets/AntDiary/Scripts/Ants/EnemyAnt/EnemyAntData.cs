using AntDiary.Scripts.Ants;
using MessagePack;

namespace AntDiary{
	[MessagePackObject()]
	
	public class EnemyAntData: StrategyAntData{
		/// <summary>
		/// 戦闘に移行する距離
		/// </summary>
		public float CombatDistance => 0.5f;

		/// <summary>
		/// 攻撃間隔
		/// 単位は秒
		/// </summary>
		/// <returns></returns>
		public float CombatInterval => 0.5f;

		/// <summary>
		/// 移動速度
		/// </summary>
		public float MovementSpeed => 1.2f;
	}
}