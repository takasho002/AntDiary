using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feedTransport : MonoBehaviour
{

    //餌の有無
    public bool isHoldingFood = false;
    //餌の位置
    public Vector2 targetFeed = GameObject.Find("feed").transform.position;
    //巣の位置
    public Vector2 targetNest = GameObject.Find("nest").transform.position;
    //現在地
    public Vector2 nowPlace = GameObject.Find("workingAnt").transform.position;
    //対象までの時間
    public float time = 10;
    //対象までの距離
    public Vector2 distance = new Vector2(0,0);
    //経路探索結果をもとにしたスピード
    public Vector2 way = new Vector2(1,1);
    //運搬能力
    public int ability = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dを取得する
       Rigidbody2D rd = GetComponent<Rigidbody2D>();
       //ありの状態の取得
       //初期位置
       nowPlace = GameObject.Find("WorkingAnt").transform.position;
       //対象までにかかる時間
       time = 10;
       //経路探索
       explore(targetFeed);
       //運搬能力
       ability = 1;
    }



    // Update is called once per frame
    void Update()
    {
        //現在地を獲得
        nowPlace = GameObject.Find("WorkingAnt").transform.position;
        //ひたすら目的地へ移動
        gotoTarget();

         // 餌の有無を確認
         if(isHoldingFood == false){ //餌がないなら
             //餌まで進む
             if(targetFeed == nowPlace){ //餌の位置まで進んだら
             //餌をゲットする
             getFeed();
             //もってる状態にして巣に帰還
             gotoTarget();
             }
         }else{ //餌持ってるなら
             if(targetNest == nowPlace){ //巣へ帰宅したら
             //餌をおく
             leaveFeed();
             gotoTarget();
             }
         }
    }


    //経路探索の結果
    public void explore(Vector2 target){
        //対象までの距離
       distance.x = target.x - nowPlace.x;
       distance.y = target.y - nowPlace.y;
       //対象までの進む道(この場合スピードと同義) これを足していく
       way.x = distance.x / time;
       way.y = distance.y / time;
    }


    //目的地に行くプログラム
    public void gotoTarget(){
        nowPlace = GameObject.Find("WorkingAnt").transform.position;
        nowPlace.x = nowPlace.x + way.x;
        nowPlace.y = nowPlace.y + way.y;
        GameObject.Find("WorkingAnt").transform.position = nowPlace;
    }


    //餌を獲得
    public void getFeed(){
        //運搬能力に応じて運べる量を決定
        //
        //巣へ帰還する道の探索
        explore(targetNest);
        //餌をゲットした状態にする
        isHoldingFood = true;
    }



    //餌を離す
    public void leaveFeed(){
        //目的地を餌に変更
        explore(targetFeed);
        //餌を持ってない状態にする
        isHoldingFood = false;
    }

}
