using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary{
	public class AStarSearcher{
		private NodeGraph _graph;

		public bool Completed{
			get;
			private set;
		}

		public List<Node> Route{
			get;
			private set;
		}

		
		

		public AStarSearcher(NodeGraph graph){
			_graph = graph;
			Route = null;
			Completed = false;
			
			
		}

		public void SearchRoute(Node from, Node to){
			Route = new List<Node>();
			if(from == to){
				Completed = true;
				return;
			}
			
			AStarNode root = new AStarNode(from, null, 0, to);
			

			AStarNode goal = SearchProcess(root);
			if(goal == null){
				Completed = true;
				return;
			}

			Debug.Log("AStarResult");
			AStarNode parent = goal;
			while(parent != null){
				Route.Add(parent.Node);
				// Debug.Log(parent);
				parent = parent.Parent;
			}
			Debug.Log("ResultFin");
			Route.Reverse();
			
			Completed = true;
		}

		private AStarNode SearchProcess(AStarNode root){ 
			List<AStarNode> openNodes = new List<AStarNode>();
			List<AStarNode> closedNodes = new List<AStarNode>();
			openNodes.Add(root);
			
			
			while(openNodes.Count > 0){
				openNodes.Sort((nodeA, nodeB) => {
					if(nodeA.TotalCost > nodeB.TotalCost) return 1;
					if(nodeA.TotalCost < nodeB.TotalCost) return -1;
					return 0;
				});

				//最小コストを選択
				AStarNode aStarNode = openNodes[0];

				foreach(var pair in aStarNode.Node.ConnectedNodes){
					var cost = aStarNode.Cost + pair.Value.GetCost;
					
					AStarNode childNode = new AStarNode(pair.Key, aStarNode, cost, aStarNode.DestNode);
					
					if(pair.Key == aStarNode.DestNode){
						return childNode;
					}
					
					openNodes.Add(childNode);
				}

				openNodes.Remove(aStarNode);
				closedNodes.Add(aStarNode);
				
			}

			return null;
		}

	}
}