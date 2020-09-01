using UnityEngine;

namespace AntDiary{
	
	public class EnemyAntFactory: AntFactory<EnemyAntData>{
		[SerializeField] private GameObject antPrefab = default;

		public override Ant InstantiateAnt(EnemyAntData antData){
			var antObject = Instantiate(antPrefab, antData.Position, Quaternion.identity);
			var ant = antObject.GetComponent<EnemyAnt>();
			ant.Initialize(antData);
			return ant;
		}
	}
}