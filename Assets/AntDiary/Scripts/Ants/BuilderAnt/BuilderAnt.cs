using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary{
	public class BuilderAnt : Ant<BuilderAntData>{
		private BuilderStrategy _strategy;
		
		
		
		void Start(){
			
		}

		// Update is called once per frame
		protected override float MovementSpeed{ get; }

		protected override void OnInitialized(){
			_strategy = new RoundStrategy(this);

			StartCoroutine("UpdateCaller");
		}

		protected override void Update(){
			
		}

		private IEnumerable UpdateCaller(){
			while(Data.IsAlive){
				_strategy.PeriodicUpdate();
				yield return new WaitForSeconds(_strategy.UpdateInterval);
			}
			_strategy.FinishStrategy();
		}
		
		public void ChangeStrategy(BuilderStrategy nextStrategy){
			_strategy?.FinishStrategy();

			_strategy = nextStrategy;
			nextStrategy.StartStrategy();
			
		}
	}

}

