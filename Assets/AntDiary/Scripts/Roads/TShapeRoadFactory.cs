using System.IO;
using AntDiary.Scripts.Roads;
using UnityEngine;

namespace AntDiary.Scripts.Roads{
	public class TShapeRoadFactory: NestElementFactory<TShapeRoadData>{
		[SerializeField] private GameObject topDirRoadPrefab;
		[SerializeField] private GameObject rightDirRoadPrefab;
		[SerializeField] private GameObject bottomDirRoadPrefab;
		[SerializeField] private GameObject leftDirRoadPrefab;

		
		public override NestElement InstantiateNestElement(TShapeRoadData elementData){
			var prefab = GetPrefab(elementData);
			var gameObj = Instantiate(prefab, elementData.Position, Quaternion.identity);
			var nestElem = gameObj.GetComponent<TShapeRoad>();
			nestElem.Initialize(elementData);

			return nestElem;
		}
		
		private GameObject GetPrefab(TShapeRoadData roadData){
			switch(roadData.Direction){
				case EnumRoadDirection.Top:
					return topDirRoadPrefab;
				case EnumRoadDirection.Right:
					return rightDirRoadPrefab;
				case EnumRoadDirection.Bottom:
					return bottomDirRoadPrefab;
				case EnumRoadDirection.Left:
					return leftDirRoadPrefab;
			}

			throw new InvalidDataException();
		}
	}
}