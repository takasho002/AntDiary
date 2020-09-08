using AntDiary.Scripts.Ants;
using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// StrategyAntの振る舞いを記述するクラス
	/// </summary>
	public abstract class Strategy<T> where T: StrategyAntData{


		/// <summary>
		/// PeriodicUpdateが呼ばれる周期
		/// 単位は秒
		/// </summary>
		public float UpdateInterval{ get; protected set; } = 1f;

		protected StrategyController<T> Controller{ get; set; }

		public T AntData{
			get => (T) Controller.Ant.Data;
		}
		
		public Strategy(){
			
		}

		/// <summary>
		/// このStrategyに変更されたときに呼ばれる
		/// </summary>
		public virtual void StartStrategy(StrategyController<T> controller){
			Controller = controller;
		}
		
		/// <summary>
		/// UpdateInterval秒ごとに定期的に呼ばれる
		/// 基本的に処理はここに書く
		/// </summary>
		public abstract void PeriodicUpdate();
		
		/// <summary>
		/// このStrategyから別のStrategyへの変更したときや、Antが死亡したときに呼ばれる
		/// </summary>
		public abstract void FinishStrategy();


		/// <summary>
		/// アリとの直線距離を返す
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public float DistanceToHostAnt(Vector2 pos){
			return Vector2.Distance(pos, Controller.Ant.transform.position);
		}

		/// <summary>
		/// posAとposBでHostAntとの距離を比較し、posAの方が近いならtrue、posBの方が近いならfalseを返す
		/// </summary>
		/// <param name="posA"></param>
		/// <param name="posB"></param>
		/// <returns></returns>
		public bool CompareDistanceToHostAnt(Vector2 posA, Vector2 posB){
			return DistanceToHostAnt(posA) < DistanceToHostAnt(posB);
		}
		
		
	}

}