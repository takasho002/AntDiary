namespace AntDiary{
	/// <summary>
	/// BuilderAntの振る舞いを記述するクラス
	/// </summary>
	public abstract class BuilderStrategy{


		/// <summary>
		/// PeriodicUpdateが呼ばれる周期
		/// 単位は秒
		/// </summary>
		public float UpdateInterval{ get; protected set; } = 1f;

		protected BuilderAnt HostAnt{ get; private set; }
		
		public BuilderStrategy(BuilderAnt ant){
			HostAnt = ant;
		}

		/// <summary>
		/// このStrategyに変更されたときに呼ばれる
		/// </summary>
		public abstract void StartStrategy();
		
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