using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using AntDiary;
using System.Diagnostics;
using System;
using System.Runtime.InteropServices.ComTypes;

public class BuildSystem : MonoBehaviour
{
    //建築に駆り出されて、建築完了するまではtrueにしておく
    public bool isWorkingToBuild = false;

    //建築に駆り出されて、現在地から建築場所まで移動するまではtrueにしておく
    //つまり移動中はtrue
    private bool isMovingToBuild = false;
    private IPathNode Way { get => way; set => way = value; }
    private int num = 0;
    public int DelaySecond = 1;
    void Start()
    {

    }

    void Update()
    {
        //建築タスクがあるか監視

        if(NestSystem.BuildingElements != null)
        {
            isWorkingToBuild = true;
            isMovingToBuildToBuild = true;
            FindRoute();
            //終わったら建築タスクから消す

        }
    }

    //ルート検索
    //IEnumerable使ってルート検索・移動する
    public void FindRoute()
    {
        //アリの現在地をfrom, 目的地をtoにする
        //FindRoute()の引数はIPathNode？でもpositionはVector型？
        //仮で宣言
        IPathNode from = ant.position;
        IPathNode to = NestBuildableElement.GetBuildingNode();
        Way = NestSystem.FindRoute(from, to);
        //ルートに沿ってアリを移動させる
        //一瞬で移動しないようにコルーチンで遅延
        StartCoroutine("DelayMethod");
        //建築を開始する
        BuildResource();

    }
    public void BuildResource()
    {
        //建築中だったらリソース注入
        //ここもコルーチンでディレイが必要？
        while (NestBuildableElement.IsUnderConstruction) {
            NestBuildableElement.Commit();
        }
        //建築終了してたらNestBuildableElement.OnBuildingCompletedが呼ばれるらしい
        //のでここで終了時の処理をする必要はない？
    }
    IEnumerator DelayMethod()
    {
        if (ant.position == NestBuildableElement.GetBuildingNode())
        {
            isMovingToBuild = false;
        }

        ant.transform.position = way[num];
        num += 1;
        return yield new WaitForSeconds(DelaySecond);
        if (isMovingToBuild)
        {
            StartCoroutine("DelayMethod");
        }
    }
}