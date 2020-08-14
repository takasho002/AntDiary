using UnityEngine;

namespace AntDiary{
	internal class AStarNode{

		internal Node Node{
			get;
		}

		internal AStarNode Parent{
			get;
		}

		internal float Cost{
			get;
		}

		internal float HeuristicCost{
			get;
		}

		internal float TotalCost => Cost + HeuristicCost;

		internal Node DestNode{
			get;
		}
		
		// public AStarNodeStatus status;

		public AStarNode(Node node, AStarNode parent, float cost, Node to){
			Node = node;
			Parent = parent;
			Cost = cost;
			DestNode = to;
			HeuristicCost = Vector2.Distance(node.Pos, to.Pos);
			
		}

		public override string ToString(){
			
			return $"node:{Node.Uid} c:{Cost} h:{HeuristicCost}: tc:{TotalCost} from:{(Parent == null ? "null" : Parent.Node.Uid.ToString())}";
		}
	}
}