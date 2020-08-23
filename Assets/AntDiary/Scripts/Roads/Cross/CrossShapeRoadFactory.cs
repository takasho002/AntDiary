using System.IO;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class CrossShapeRoadFactory: NestElementFactory<CrossShapeRoadData>{
		[SerializeField] private GameObject roadPrefab;
		
		
		public override NestElement InstantiateNestElement(CrossShapeRoadData elementData){
			var gameObj = Instantiate(roadPrefab, elementData.Position, Quaternion.identity);
			var nestElem = gameObj.GetComponent<CrossShapeRoad>();
			nestElem.Initialize(elementData);

			return nestElem;
		}
	}
}