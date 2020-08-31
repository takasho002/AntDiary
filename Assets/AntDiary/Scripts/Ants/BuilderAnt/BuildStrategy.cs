using AntDiary.Scripts.Ants;
using UnityEngine;

namespace AntDiary{
	public class BuildStrategy: Strategy<BuilderAntData>{
		private NestBuildableElement _distElement;

		private float CommitResourcePerSec{ get; } = 3.0f;
		
		public BuildStrategy(NestBuildableElement distElement) : base(){
			_distElement = distElement;

			UpdateInterval = 1.0f;
		}


		public override void PeriodicUpdate(){
			Debug.Log("<BuildStrategy> commit");
			_distElement.Commit(CommitResourcePerSec);

			if(!_distElement.IsUnderConstruction){
				Controller.ChangeStrategy(new RoundStrategy());
			}
		}

		public override void FinishStrategy(){
			
		}
	}
}