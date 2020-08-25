using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AntDiary;
using System.Linq;

public class MainDebug : MonoBehaviour
{
    private NestSystem nestsystem => NestSystem.Instance;
    [SerializeField] bool create = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (create)
        {
            CreateFeedRoute();
            create = false;
        }
    }

    public void CreateFeedRoute()
    {
        

        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 4; x++)
            {
                float posx = x * 4f - 1.5f * 4f;
                float posy = y * 3f - 1f * 3f;
                nestsystem.InstantiateNestElement(new DebugRoomData()
                { Position = new Vector2(posx, posy), IsUnderConstruction = false });
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
    }
}
