using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary{
	public class BuilderAntFactory : AntFactory<BuilderAntData>{
		
		[SerializeField] private GameObject antPrefab = default;

		public override Ant InstantiateAnt(BuilderAntData antData){
			var antObject = Instantiate(antPrefab, antData.Position, Quaternion.identity);
			var ant = antObject.GetComponent<BuilderAnt>();
			ant.Initialize(antData);
			return ant;
		}
	}

}
