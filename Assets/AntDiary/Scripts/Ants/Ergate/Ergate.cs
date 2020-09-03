using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AntDiary;
using System.Linq;

//餌運びプログラム
//同じところを往復するプログラムを作成
//違うところに帰りたければ修正が必要
//
public class Ergate : MonoBehaviour
{
    //MoveAntの取得
    AntMovement antMovement;
    ErgateAntData data;

    //餌の有無
    //public bool isHoldingFood => Ant.Data.IsHoldingFood;
    //目的地の定義
    public Vector2 targetFeed;
    public Vector2 targetNest;
    public Vector2 targetNestEntrance;
    public Vector2 target;
    public Vector2 from;

    //public Vector2 nowPlace;

    //AStarへのnodeの初期化
    //IPathNode searcher;
    // 探索結果をもとに曲がり角の座標を保存するリスト型配列

    public NestPathNode nodeNest;
    public NestPathNode nodeFeed;
    public List<Vector2> v;
    //その引数i
    public int i;
    //リスト型配列の大きさI
    public int I = -1;

    //対象までのスピード    public float speed;
    //対象までの距離
    public Vector2 distance;

    void Start()
    {
        data = (ErgateAntData)GetComponent<ErgateAnt>().Data;

        antMovement = GetComponent<AntMovement>();
        //目的地の設定
        var elements = NestSystem.Instance.NestElements;
        Debug.Log(NestSystem.Instance.NestElements.Count);
        foreach (var element in elements)
        {
            if (element.GetType().Name.Equals("StoreRoom"))
            {
                nodeNest = element.GetNodes().ElementAt(0);
            }
            /*
            else if (element.GetType().Name.)
            {

            }
            */
        }
        nodeFeed = NestSystem.Instance.NestElements[1].GetNodes().ElementAt(1);

       //餌と巣の位置情報はテスト用(後で削除または変更する可能性あり)
       //現在置
       //自身の座標を取得
        //antMovement.nowPlace = transform.position;

        //探索ごとにAStarSearcherインスタンスを作る
        IEnumerable<IPathNode> routes = NestSystem.Instance.FindRoute(nodeNest, nodeFeed);

        //結果はsearcher.Routeに入る
        foreach (var node in routes)
        {
            Debug.Log(node.WorldPosition);
            v.Add(node.WorldPosition);
            I++;
        }

        //ありの状態の取得
        //初期位置
        antMovement.nowPlace = transform.position;
        //アリのスピード取得(未取得)
        //public float speed = hogehoge.speed;
        //試験用スピード //これは後で削除
        antMovement.speed = 0.01f;
        //node間の移動を計算し足していく値
        //最初は1番目の目的地へと向かう
        i = 1;
        target = v[i];
        antMovement.way = antMovement.Explore(target);

    }



    // Update is called once per frame
    
    void Update()
    {
        //現在地を獲得
        antMovement.nowPlace = transform.position;
        //アリの現在地がtarget周辺についたら
        if (antMovement.isArrived(target))
        {
            //餌を持ってなければ
            if (data.IsHoldingFood == false)
            {
                //餌場じゃなければ               
                if (target != v[I])
                {
                    //次の目的地へ移動させる
                    i++;
                    target = v[i];

                    antMovement.way = antMovement.Explore(target);
                    // gotoTarget();
                }
                else
                {//餌場ならば
                 //餌をゲットする
                    getFeed();
                }
            }
            else
            { //餌を持っているなら
              //目的地が巣ではないのなら
                if (target != v[0])
                {
                    //巣へ近づけさせる
                    i--;
                    target = v[i];
                    antMovement.way = antMovement.Explore(target);
                }
                else
                {//巣に到着したら
                 //餌をおく
                    leaveFeed();
                }
            }
        }
        //Explore()の結果に従い移動する
        antMovement.nowPlace += antMovement.way;
        transform.position = antMovement.nowPlace;
    }
    
    //餌を獲得
    public void getFeed()
    {
        //巣へ帰還する道の探索
        from = v[i];
        i--;
        target = v[i];
        antMovement.way = antMovement.Explore(target);
        //経路探索をする
        //餌をゲットした状態にする
        data.IsHoldingFood = true;
    }

    //餌を離す
    public void leaveFeed()
    {
        //目的地を餌に変更
        i++;
        target = v[i];
        antMovement.way = antMovement.Explore(target);
        //経路探索をする
        //餌を持ってない状態にする
        data.IsHoldingFood = false;
    }

}