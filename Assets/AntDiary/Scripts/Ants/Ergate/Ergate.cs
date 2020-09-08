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
    public ErgateAnt HostAnt;
    public NestPathNode nodeNest;
    public NestPathNode nodeFeed;

    // public Vector2 distance;

         
    void Start()
    {
        data = (ErgateAntData)GetComponent<ErgateAnt>().Data;
        //HostAnt = new ErgateAnt();
        HostAnt = (ErgateAnt)GetComponent<ErgateAnt>();
        //目的地の設定
        var elements = NestSystem.Instance.NestElements;
        Debug.Log(elements.Count);
        foreach (var element in elements)
        {
            if (element.GetType().Name.Equals("StoreRoom"))
            {
                nodeNest = element.GetNodes().ElementAt(0);
            }
        }
        nodeFeed = NestSystem.Instance.NestElements[2].GetNodes().FirstOrDefault(n => n.Name == "wild_top");
        if(nodeFeed ==null)Debug.Log("nullです");
        HostAnt.transform.localScale = new Vector3(1/2f, 1/2f, 1);
        Vector3 a = HostAnt.transform.position;
        a.z = -1;
        HostAnt.transform.position = a;
        ErgateAntStart(nodeFeed);
    }

    private void Cancel(){
        Debug.Log("Canceled");
        HostAnt.CancelMovement();
    }
    // Update is called once per frame
    public void Update()
    {
    }
    
    //餌を獲得
    private void GetFeed()
    {
        data.IsHoldingFood = true;
        Debug.Log("We got feed!!!");
        //巣に帰還
        HostAnt.StartForPathNode(nodeNest , LeaveFeed , Cancel);
    }

    //餌を離す
    private void LeaveFeed()
    {
        //餌を持ってない状態にする
        data.IsHoldingFood = false;
        Debug.Log("Finish!");
    }

    //Debug用スタートプログラム
    private void ErgateAntStart(NestPathNode nodeFeed){
        Debug.Log("start!");
        HostAnt.StartForPathNode(nodeFeed , LeaveFeed , Cancel);
        
    }
}

