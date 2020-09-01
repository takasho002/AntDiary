using AntDiary.Scripts.Ants;

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
			// HostAnt = ant;
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

		
		
		
	}

}