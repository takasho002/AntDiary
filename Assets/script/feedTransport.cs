using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feedTransport : MonoBehaviour
{

    //餌の有無
    public bool isHoldingFood = false;
    //目的地の定義
    public Vector2 targetFeed;
    public Vector2 targetNest;
    public Vector2 nowPlace;
    //対象までかかる時間
    public int time = 10;
    //対象までの距離
    public Vector2 distance = new Vector2(0,0);
    //経路探索結果をもとにしたスピード
    public Vector2 way = new Vector2(2,2);
    //運搬能力
    //public int ability = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dを取得する
       Rigidbody2D rd = GetComponent<Rigidbody2D>();
       //目的地の設定
       //餌の位置
       targetFeed = GameObject.Find("feed").transform.position;
       //巣の位置
       targetNest = GameObject.Find("nest").transform.position;
       //現在置
       nowPlace = GameObject.Find("workingAnt").transform.position;
       //ありの状態の取得
       //初期位置
       nowPlace = GameObject.Find("workingAnt").transform.position;
       //対象までにかかる時間
       time = 1000;
       //経路探索
       way = explore(targetFeed);
       //運搬能力
       //ability = 1;
    }



    // Update is called once per frame
    void Update()
    {
        //現在地を獲得
        nowPlace = transform.position;
        //ひたすら目的地へ移動
        gotoTarget();

         // 餌の有無を確認
         if(isHoldingFood == false){ //餌がないなら
             //餌まで進む
             if(targetFeed.x-1/2 <= nowPlace.x && targetFeed.x+1/2 >= nowPlace.x && targetFeed.y-1/2 <= nowPlace.y && targetFeed.y+1/2 >= nowPlace.y){ //餌の位置まで進んだら
             //餌をゲットする
             getFeed();
             //もってる状態にして巣に帰還
             gotoTarget();
             }
         }else{ //餌持ってるなら
             if(targetNest.x-1/2 <= nowPlace.x && targetNest.x+1/2 >= nowPlace.x && targetNest.y-1/2 <= nowPlace.y && targetNest.y+1/2 >= nowPlace.y){ //巣へ帰宅したら
             //餌をおく
             leaveFeed();
             gotoTarget();
             }
         }
    }


    //経路探索の結果
    public Vector2 explore(Vector2 target){
        //対象までの距離
       distance = target - nowPlace;
       //対象までの進む道(この場合スピードと同義) これを足していく
       way = distance / time;
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
        way = explore(targetNest);
        //餌をゲットした状態にする
        isHoldingFood = true;
    }



    //餌を離す
    public void leaveFeed(){
        //目的地を餌に変更
        way = explore(targetFeed);
        //餌を持ってない状態にする
        isHoldingFood = false;
    }

}
