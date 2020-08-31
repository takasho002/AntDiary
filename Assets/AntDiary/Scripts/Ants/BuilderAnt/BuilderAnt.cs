using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary{
	public class BuilderAnt : Ant<BuilderAntData>{
		private BuilderStrategy _strategy;

		// Update is called once per frame
		protected override float MovementSpeed => 1.0f;

		protected override void OnInitialized(){
			ChangeStrategy(new RoundStrategy(this));

			Debug.Log("BuilderAnt Initialized");

			var coroutine = UpdateCaller();
			StartCoroutine(coroutine);
		}

		private int _counter;
		private IEnumerator UpdateCaller(){
			Debug.Log("UpdateCaller");
			while(Data.IsAlive){
				Debug.Log("Update: " + _counter++);
				_strategy.PeriodicUpdate();
				yield return new WaitForSeconds(_strategy.UpdateInterval);
			}
			Debug.Log("Dead");
			_strategy.FinishStrategy();
		}
		
		public void ChangeStrategy(BuilderStrategy nextStrategy){
			Debug.Log("ChangeStrategy");
			_strategy?.FinishStrategy();

			_strategy = nextStrategy;
			nextStrategy.StartStrategy();
			
		}
	}

}

