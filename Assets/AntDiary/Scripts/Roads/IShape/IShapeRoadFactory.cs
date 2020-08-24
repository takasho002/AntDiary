using System.IO;
using AntDiary.Scripts.Roads;
using UnityEngine;

namespace AntDiary{
	public class IShapeRoadFactory: NestElementFactory<IShapeRoadData>{
		
		[SerializeField] private GameObject roadPrefabHorizontal;
		[SerializeField] private GameObject roadPrefabVertical;

		public override NestElement InstantiateNestElement(IShapeRoadData roadData){
			var prefab = GetPrefab(roadData);
			var gameObj = Instantiate(prefab, roadData.Position, Quaternion.identity);
			var nestElem = gameObj.GetComponent<IShapeRoad>();
			nestElem.Initialize(roadData);

			return nestElem;
		}

		private GameObject GetPrefab(IShapeRoadData roadData){
			switch(roadData.Direction){
				case EnumRoadHVDirection.Horizontal:
					return roadPrefabHorizontal;
				case EnumRoadHVDirection.Vertical:
					return roadPrefabVertical;
			}

			throw new InvalidDataException();
		}
	}
}