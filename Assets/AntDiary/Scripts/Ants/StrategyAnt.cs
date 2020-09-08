using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Ants{
	
	/// <summary>
	/// Strategyによって振る舞いが変わるアリ
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class StrategyAnt<T>: Ant<T> where T : StrategyAntData{
		
		public StrategyController<T> Controller{ get; private set; }

		public string GetCurrentStrategyName(){
			return Controller.GetStrategyName();
		}
		
		protected override void OnInitialized(){
			Debug.Log("StrategyAnt Initialized");
			
			Controller = new StrategyController<T>(this, CreateInitialStrategy());
		}

		protected abstract Strategy<T> CreateInitialStrategy();
		
	}
}