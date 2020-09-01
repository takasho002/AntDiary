using UnityEngine;

namespace AntDiary{
	public class SoldierAntFactory: AntFactory<SoldierAntData>{
		
		[SerializeField] private GameObject antPrefab = default;

		public override Ant InstantiateAnt(SoldierAntData antData){
			var antObject = Instantiate(antPrefab, antData.Position, Quaternion.identity);
			var ant = antObject.GetComponent<SoldierAnt>();
			ant.Initialize(antData);
			return ant;
		}
	}
}