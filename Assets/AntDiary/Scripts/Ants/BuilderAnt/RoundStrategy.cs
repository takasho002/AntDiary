using System;
using System.Collections.Generic;
using System.Linq;
using AntDiary.Scripts.Ants;
using UnityEngine;

namespace AntDiary{
	/// <summary>
	/// 建築中のElementがなく、待機してるときのStrategy
	/// </summary>
	public class RoundStrategy: Strategy<BuilderAntData>{
		public RoundStrategy() : base(){
			UpdateInterval = 1.0f;
		}

		

		public override void PeriodicUpdate(){
			//到達可能なのはすでに建築済みのNodeだけだが、欲しいのはそのNodeに重なった建築中のNode
			
			var buildingElements = NestSystem.Instance.BuildingElements;
			var currentNode = Controller.Ant.SupposeCurrentPosNode();
			
			Debug.Log($"<RoundStrategy> PeriodicUpdate building?: {buildingElements.Any()} ant:{currentNode?.WorldPosition} on {currentNode?.Host.transform.position}");
			
			if(!buildingElements.Any()) return;
			if(currentNode == null) return;
			

			//建築中のElementの各ノードとそのホスト(建築中)をペアにして、それを現在位置との直線距離の短い順でソート
			//TODO 厳密には移動コスト順にすべき
			var buildingNodes = buildingElements
				.SelectMany(elem => elem.GetBuildingNode().Select(node => new Tuple<NestBuildableElement, NestPathNode>(elem, node as NestPathNode)).ToList())
				.OrderBy(tuple => Vector3.Distance(tuple.Item2.WorldPosition, currentNode.WorldPosition));
			
			
			// Debug.Log("buildingNodes");
			// foreach(var tuple in buildingNodes){
			// 	Debug.Log($"  [{tuple.Item2.WorldPosition}] name:{tuple.Item2.Name} host:{tuple.Item1.transform.position}");
			// }
			
			var distTuple = buildingNodes.FirstOrDefault(tuple =>
				NestSystem.Instance.FindRoute(tuple.Item2, currentNode).Count() >= 2);
			
			
			Controller.ChangeStrategy(new MoveStrategy(distTuple.Item1, distTuple.Item2));


		}

		public override void FinishStrategy(){
			
		}
	}
}