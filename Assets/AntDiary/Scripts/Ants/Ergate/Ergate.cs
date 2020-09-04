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
    ErgateAntData data;
    ErgateAnt Host;
    public NestPathNode nodeNest;
    public NestPathNode nodeFeed;

    // public Vector2 distance;

    void Start()
    {
        data = (ErgateAntData)GetComponent<ErgateAnt>().Data;
        Host = ;
        //antMovement = GetComponent<AntMovement>();

        //目的地の設定
        var elements = NestSystem.Instance.NestElements;
        Debug.Log(NestSystem.Instance.NestElements.Count);
        foreach (var element in elements)
        {
            if (element.GetType().Name.Equals("StoreRoom"))
            {
                nodeNest = element.GetNodes().ElementAt(0);
            }
        }


        nodeFeed = NestSystem.Instance.NestElements[1].GetNodes().ElementAt(1);
        Host.StartForPathNode(nodeFeed , getFeed , Cancel);
    }

    void Cancel(){
        Host.CancelMovement();
    }
    // Update is called once per frame
    void Update()
    {
    }
    
    //餌を獲得
    public void getFeed()
    {
        data.IsHoldingFood = true;
        //巣に帰還
        Host.StartForPathNode(nodeNest , leaveFeed , Cancel);
    }

    //餌を離す
    public void leaveFeed()
    {
        //餌を持ってない状態にする
        data.IsHoldingFood = false;
    }

}