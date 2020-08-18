using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AntDiary;
//using hogehoge;　//アリ情報の取得プログラム

//餌運びプログラム
//同じところを往復するプログラムを作成
//違うところに帰りたければ修正が必要
//
public class feedTransport : MonoBehaviour
{

    //餌の有無
    public bool isHoldingFood = false;
    //目的地の定義
    public Vector2 targetFeed;
    public Vector2 targetNest;
    public Vector2 targetNestEntrance;
    public Vector2 target;
    public Vector2 from;

    public Vector2 nowPlace;

    //AStarへのnodeの初期化
    　 //経路探索のプログラムにぶち込む為の操作
    　 //経路全体を示す変数名"nodeGraph"
       NodeGraph nodeGraph;

       //試験的に3つのnodoを作ってみる(実際には使わない)
       Node nodeNest;
	   Node nodeFeed;
	   Node nodeNestEntrance;


       //探索ごとにAStarSearcherインスタンスを作る
       //ここに経路探索の結果を入れる
       AStarSearcher searcher;

       // 探索結果をもとに曲がり角の座標を保存するリスト型配列
       public List<Vector2> v;
       //その引数i
    　　public int i;
        //リスト型配列の大きさI
        public int I=-1;

    //
    //対象までかかる時間
    public float speed;
    //対象までの距離
    public Vector2 distance = new Vector2(0,0);
    //経路探索結果をもとにしたスピード
    public Vector2 way = new Vector2(2,2);
    //運搬能力
    //public int ability = 1;
    
    
    // Start is called before the first frame update
    
    void Start()
    {
       //目的地の設定

       //餌と巣の位置情報はテスト用(後で削除または変更する可能性あり)
       //餌の位置
       targetFeed = GameObject.Find("feed").transform.position;
       //巣の位置
       targetNest = GameObject.Find("nest").transform.position;
       //現在置
       //自身の座標を取得
       nowPlace = transform.position;

       //経路探索のプログラムにぶち込むためのnode作成
       nodeGraph = new NodeGraph();
       nodeNest = nodeGraph.CreateNode(targetNest);
	   nodeFeed = nodeGraph.CreateNode(targetFeed);
       //現在は曲がり角を生成していないため、nodeは2つだけ
       //試しにtargetNestEntrance（巣の出入り口）なるものを曲がり角として用意してみる
       targetNestEntrance = GameObject.Find("nestEntrance").transform.position;
       nodeNestEntrance = nodeGraph.CreateNode(targetNestEntrance);
       //nodeをそれぞれつなげる作業
       //nodeGraph.ConnectNodes(new []{from,to});→ fromからtoへnodeをつなげる
       //nodeGraph.ConnectNodes(new []{A, B,C});→ABCの循環円を作成
       nodeGraph.ConnectNodes(new []{nodeNest, nodeNestEntrance});
       nodeGraph.ConnectNodes(new []{nodeNestEntrance,nodeFeed});
       //探索ごとにAStarSearcherインスタンスを作る
       searcher = new AStarSearcher(nodeGraph);
       //経路探索をする
	   searcher.SearchRoute(nodeNest, nodeFeed);
       Debug.Log(searcher.Route);
       //結果はsearcher.Routeに入る
		foreach(var node in searcher.Route){
            //vにnode.Posで値を取得する
            v.Add(node.Pos);
			Debug.Log(node.ToString());
            I++; //vの大きさ
		}


       //ありの状態の取得
       //初期位置
       nowPlace = transform.position;
       //アリのスピード取得(未取得)
       //public float speed = hogehoge.speed;
       //試験用スピード //これは後で削除
       speed = 0.01f;
       //node間の移動を計算し足していく値
       //最初は1番目の目的地へと向かう
       i = 1;
       target = v[i];
       way = explore(target);
       //float x = way.x;
       //float y = way.y;
       //Debug.Log(x);
       //Debug.Log(y);
       //運搬能力
       //ability = 1;
    }



    // Update is called once per frame
    void Update()
    {
        //現在地を獲得
        nowPlace = transform.position;
        //ひたすら目的地へ移動
        
        //アリの現在地がtarget周辺についたら
        if(target.x -1 < nowPlace.x && target.y -1 < nowPlace.y && nowPlace.x < target.x + 1&& nowPlace.y < target.y + 1 ){
           //餌を持ってなければ
           if(isHoldingFood == false){
               //餌場じゃなければ               
            if(target != v[I]){ 
                //次の目的地へ移動させる
                i++;
                target = v[i];

                way = explore(target);
                Debug.Log("次の目的地は" + way);
                // gotoTarget();
            }else{//餌場ならば
             //餌をゲットする
             getFeed();
             //もってる状態にして巣に帰還
            //  gotoTarget();
            }
           }else{ //餌を持っているなら
           //目的地が巣ではないのなら
             if(target != v[0]){
                //巣へ近づけさせる
                i--;
                target = v[i];
                way = explore(target);
                }else{//巣に到着したら
                //餌をおく
                leaveFeed();
               }
           }   
        }
        gotoTarget();
    }


    //経路探索の結果をもとにありんこに足すベクトルの計算
    public Vector2 explore(Vector2 target){
        //対象までの距離
       distance = target - nowPlace;
       //対象までの進む道(この場合スピードと同義) これを足していく
       way.x = speed* distance.x/distance.magnitude;
       way.y = speed* distance.y/distance.magnitude;
       return way;
    }


    //目的地に行くプログラム
    public void gotoTarget(){
        nowPlace = transform.position;
        nowPlace = nowPlace + way;
        transform.position = nowPlace;
    }


    //餌を獲得
    public void getFeed(){
        //運搬能力に応じて運べる量を決定
        //今は省略
        //巣へ帰還する道の探索
        from = v[i];
        i--;
        target = v[i];
        way = explore(target);
        //経路探索をする
		//searcher.SearchRoute(nodeFeed, nodeNest);
        //餌をゲットした状態にする
        isHoldingFood = true;
    }

    //餌を離す
    public void leaveFeed(){
        //目的地を餌に変更
        i++;
        target=v[i];
        way = explore(target);
        //経路探索をする
		//searcher.SearchRoute(nodeNest, nodeFeed);
        //餌を持ってない状態にする
        isHoldingFood = false;
    }

}
