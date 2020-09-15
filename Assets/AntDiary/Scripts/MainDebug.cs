using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AntDiary;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class MainDebug : MonoBehaviour
{
    private NestSystem nestsystem => NestSystem.Instance;
    [SerializeField] bool createNest = false;
    [SerializeField] bool createErgate = false;
    [SerializeField] bool createQueen = false;
    [SerializeField] bool createBuilder = false;
    [SerializeField] bool createFree = false;
    [SerializeField] bool createSoldier = false;
    [SerializeField] bool createEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (createNest)
        {
            createNest = false;
            CreateFeedRoute();
        }
        if (createErgate)
        {
            createErgate = false;
            CreateErgate();
        }
        if (createQueen)
        {
            createQueen = false;
            CreateQueen();
        }
        if (createBuilder)
        {
            createBuilder = false;
            CreateBuilder();
        }
        if (createFree)
        {
            createFree = false;
            CreateFree();
        }
        if (createSoldier)
        {
            createSoldier = false;
            CreateSoldier();
        }
        if (createEnemy)
        {
            createEnemy = false;
            CreateEnemy();
        }
    }

    private void CreateErgate()
    {
        ErgateAntData data = new ErgateAntData() {IsHoldingFood = true };
        nestsystem.InstantiateAnt(data);
    }
    private void CreateQueen()
    {
        AntData data = new QueenAntData();
        nestsystem.InstantiateAnt(data);
    }

    private void CreateBuilder()
    {
        AntData data = new BuilderAntData();
        nestsystem.InstantiateAnt(data);
    }
    private void CreateFree()
    {
        AntData data = new UnemployedAntData();
        nestsystem.InstantiateAnt(data);
    }

    private void CreateSoldier()
    {
        AntData data = new SoldierAntData();
        nestsystem.InstantiateAnt(data);
    }
    private void CreateEnemy()
    {
        AntData data = new EnemyAntData();
        nestsystem.InstantiateAnt(data);
    }
    public void CreateFeedRoute()
    {
        
        /*
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 4; x++)
            {
                float posx = x * 4f - 1.5f * 4f;
                float posy = y * 3f - 1f * 3f;
                //右下に貯蔵庫作成
                if(y==2 && x == 3)
                {
                    nestsystem.InstantiateNestElement(new StoreRoomData()
                    { Position = new Vector2(posx, posy), IsUnderConstruction = false });
                }
                else
                {
                    nestsystem.InstantiateNestElement(new DebugRoomData()
                    { Position = new Vector2(posx, posy), IsUnderConstruction = false });
                }
            }

        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
            {
                int i = y * 4 + x;
                var n1 = nestsystem.NestElements[i].GetNodes().First(n => n.Name == "right");
                var n2 = nestsystem.NestElements[i + 1].GetNodes().First(n => n.Name == "left");

                var road = nestsystem.InstantiateNestElement(new DebugRoadData()
                { From = n1.WorldPosition, To = n2.WorldPosition, IsUnderConstruction = false });
                var roadNodes = road.GetNodes();
                var r1 = roadNodes.ElementAt(0);
                var r2 = roadNodes.ElementAt(1);

                nestsystem.ConnectElements(n1, r1);
                nestsystem.ConnectElements(r2, n2);
            }


        for (int y = 0; y < 2; y++)
            for (int x = 0; x < 4; x++)
            {
                int i = y * 4 + x;
                var n1 = nestsystem.NestElements[i].GetNodes().First(n => n.Name == "top");
                var n2 = nestsystem.NestElements[i + 4].GetNodes().First(n => n.Name == "bottom");

                var road = nestsystem.InstantiateNestElement(new DebugRoadData()
                { From = n1.WorldPosition, To = n2.WorldPosition, IsUnderConstruction = false });
                var roadNodes = road.GetNodes();
                var r1 = roadNodes.ElementAt(0);
                var r2 = roadNodes.ElementAt(1);

                nestsystem.ConnectElements(n1, r1);
                nestsystem.ConnectElements(r2, n2);
            }
        */
        //巣の入り口及び砂糖の山の生成
        nestsystem.InstantiateNestElement(new MtSugarData()
        { Position = new Vector2(-2.5f, 3.0f), IsUnderConstruction = false });
        nestsystem.InstantiateNestElement(new GroundData()
        { Pos = new Vector2(-1.0f,3.0f), IsUnderConstruction = false });
        var sugar = nestsystem.NestElements[0].GetNodes().ElementAt(1);
        var ground = nestsystem.NestElements[1].GetNodes().ElementAt(0);
        //if (ground.IsExposed) Debug.Log("hoho");
        nestsystem.ConnectElements(sugar, ground);
    }
}
