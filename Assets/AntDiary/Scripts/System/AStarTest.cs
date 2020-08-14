using System.Collections;
using System.Collections.Generic;
using AntDiary;
using UnityEngine;

public class AStarTest : MonoBehaviour{
	// Start is called before the first frame update
	void Start(){
		Debug.Log("testStart");
		
		//経路グラフはNodeGraphで表現する
		NodeGraph nodeGraph = new NodeGraph();
		Node nodeA = nodeGraph.CreateNode(new Vector2(0, 0 ));
		Node nodeB = nodeGraph.CreateNode(new Vector2(-5, 10 ));
		Node nodeC = nodeGraph.CreateNode(new Vector2(0, 5 ));
		Node nodeD = nodeGraph.CreateNode(new Vector2(10, 10 ));
		Node nodeE = nodeGraph.CreateNode(new Vector2(10, 0 ));
		
		//それぞれをつなげる
		nodeGraph.ConnectNodes(new []{nodeA, nodeB, nodeC});
		nodeGraph.ConnectNodes(new []{nodeB, nodeC, nodeD});
		nodeGraph.ConnectNodes(new []{nodeA, nodeE});
		nodeGraph.ConnectNodes(new []{nodeD, nodeE});

		Debug.Log(nodeA.ToString());
		Debug.Log(nodeB.ToString());
		Debug.Log(nodeC.ToString());
		Debug.Log(nodeD.ToString());
		Debug.Log(nodeE.ToString());
		
		
		Debug.Log("graphOk");
		
		Debug.Log("AStarSearchStart");
		var stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		//探索ごとにAStarSearcherインスタンスを作る
		AStarSearcher searcher = new AStarSearcher(nodeGraph);
		
		//経路探索をする
		searcher.SearchRoute(nodeB, nodeE);
		
		stopWatch.Stop();
		Debug.Log(stopWatch.ElapsedMilliseconds + "ms");
		
		Debug.Log("Route:");
		//結果はsearcher.Routeに入る
		foreach(var node in searcher.Route){
			Debug.Log(node.ToString());
		}
		// Debug.Log();
	}

}