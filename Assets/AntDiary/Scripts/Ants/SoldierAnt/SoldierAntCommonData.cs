using MessagePack;

namespace AntDiary{
	
	[MessagePackObject()]
	public class SoldierAntCommonData : AntCommonData<SoldierAntData>{
		/// <summary>
		/// 戦闘に移行する距離
		/// </summary>
		[Key(10)] public float CombatDistance => 0.5f;

		/// <summary>
		/// 攻撃間隔
		/// 単位は秒
		/// </summary>
		/// <returns></returns>
		[Key(11)] public float CombatInterval => 0.5f;
	}
}