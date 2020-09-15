using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Ants{
	/// <summary>
	/// StrategyAntのStrategyを管理する部分
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StrategyController<T> where T: StrategyAntData{
		
		public StrategyAnt<T> Ant{ get; private set; }
		private Strategy<T> _currentStrategy;

		public StrategyController(StrategyAnt<T> ant, Strategy<T> initialStrategy){
			var coroutine = UpdateCaller();
			Ant = ant;
			ChangeStrategy(initialStrategy);
			ant.StartCoroutine(coroutine);
		}
		
		private IEnumerator UpdateCaller(){
			Debug.Log("UpdateCaller");
			while(Ant.Data.IsAlive){
				// Debug.Log("Update: " + ++);
				_currentStrategy.PeriodicUpdate();
				yield return new WaitForSeconds(_currentStrategy.UpdateInterval);
			}
			Debug.Log("Dead");
			_currentStrategy.FinishStrategy();
		}
		
		/// <summary>
		/// Strategyを変更する
		/// </summary>
		/// <param name="nextStrategy"></param>
		public void ChangeStrategy(Strategy<T> nextStrategy){
			Debug.Log("ChangeStrategy");
			_currentStrategy?.FinishStrategy();

			_currentStrategy = nextStrategy;
			nextStrategy.StartStrategy(this);
			
		}

		/// <summary>
		/// 現在のStrategyのクラス名を取得する
		/// </summary>
		/// <returns></returns>
		public string GetStrategyName(){
			return _currentStrategy.GetType().Name;
		}
	}
}