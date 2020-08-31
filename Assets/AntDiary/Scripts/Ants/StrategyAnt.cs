using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary.Scripts.Ants{
	
	public abstract class StrategyAnt<T>: Ant<T> where T : StrategyAntData{
		
		public StrategyController<T> Controller{ get; private set; }
		
		
		protected override void OnInitialized(){
			Debug.Log("StrategyAnt Initialized");
			
			Controller = new StrategyController<T>(this, InitialStrategy());
		}

		protected abstract Strategy<T> InitialStrategy();
	
	}
}