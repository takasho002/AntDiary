using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// 戦闘関係のユーティリティ
	/// </summary>
	public class CombatUtil{
		//シングルトンにしてMainSystemの下に生やすか悩んだけど、今のところ状態持たないのでUtilにした

		public static void AttackToAnt(Ant from, Ant target){
			var damage = from.CommonData.BasicEfficiency;

			target.Data.Health -= damage;
		}

		
	}
}