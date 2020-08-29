using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AntDiary;
using AntDiary.Scripts.Roads;
using UnityEngine;

public class TestRoadInstantiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        NestElement[,] map = new NestElement[5,6];
        
       

        map[0, 0] = InstantiateLShapeRoad(EnumRoadDirection.Right);
        map[0, 1] = InstantiateLShapeRoad(EnumRoadDirection.Bottom);
        map[0, 2] = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        map[0, 3] = InstantiateLShapeRoad(EnumRoadDirection.Bottom);

        map[1, 0] = InstantiateIShapeRoad(EnumRoadHVDirection.Vertical);
        map[1, 1] = InstantiateTShapeRoad(EnumRoadDirection.Right);
        map[1, 2] = InstantiateLShapeRoad(EnumRoadDirection.Bottom);
        map[1, 3] = InstantiateTShapeRoad(EnumRoadDirection.Right);
        map[1, 4] = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        map[1, 5] = InstantiateLShapeRoad(EnumRoadDirection.Bottom);

        map[2, 0] = InstantiateIShapeRoad(EnumRoadHVDirection.Vertical);
        map[2, 1] = InstantiateTShapeRoad(EnumRoadDirection.Right);
        map[2, 2] = InstantiateCrossShapeRoad();
        map[2, 3] = InstantiateLShapeRoad(EnumRoadDirection.Left);
        map[2, 4] = InstantiateLShapeRoad(EnumRoadDirection.Right);
        map[2, 5] = InstantiateLShapeRoad(EnumRoadDirection.Left);

        map[3, 0] = InstantiateLShapeRoad(EnumRoadDirection.Top);
        map[3, 1] = InstantiateTShapeRoad(EnumRoadDirection.Top);
        map[3, 2] = InstantiateCrossShapeRoad();
        map[3, 3] = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        map[3, 4] = InstantiateCrossShapeRoad();
        map[3, 5] = InstantiateLShapeRoad(EnumRoadDirection.Left);

        map[4, 2] = InstantiateLShapeRoad(EnumRoadDirection.Top);
        map[4, 3] = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        map[4, 4] = InstantiateTShapeRoad(EnumRoadDirection.Top);
        map[4, 5] = InstantiateLShapeRoad(EnumRoadDirection.Bottom);

        for(int i = 0; i < map.GetLength(0); i++){
            for(int j = 0; j < map.GetLength(1); j++){
                if(map[i, j] == null){
                    continue;
                }
                map[i, j].transform.position -= new Vector3((j-3)*-4, (i-3)*4);
            }
        }

       

        //横接続
        for(int i = 0; i < map.GetLength(0); i++){
            for(int j = 0; j < map.GetLength(1) - 1; j++){
                var elemA = map[i, j]?.GetNodes().First(n => n.Name == "right");
                var elemB = map[i, j+1]?.GetNodes().First(n => n.Name == "left");
                if(elemA != null && elemB != null){
                    NestSystem.Instance.ConnectElements(elemA, elemB);
                }
            }
        }
        
        //縦接続
        for(int i = 0; i < map.GetLength(0) - 1; i++){
            for(int j = 0; j < map.GetLength(1); j++){
                var elemA = map[i, j]?.GetNodes().First(n => n.Name == "top");
                var elemB = map[i+1, j]?.GetNodes().First(n => n.Name == "bottom");
                if(elemA != null && elemB != null){
                    NestSystem.Instance.ConnectElements(elemA, elemB);
                }
            }
        }
        
        
        
        
        //IShape
        // Debug.Log("Horizontal");
        // var roadA = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        // var roadB = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        // roadB.transform.position += Vector3.right * 5f;
        //
        // Debug.Log("canPlace: " + NestSystem.Instance.BuildingSystem.Instance.CanPlaceable(roadB));

        //LShape
        // var roadC = InstantiateLShapeRoad(EnumRoadDirection.Top);
        // var roadD = InstantiateLShapeRoad(EnumRoadDirection.Right);
        // roadD.transform.position += Vector3.down * 4f;
        // var roadE = InstantiateLShapeRoad(EnumRoadDirection.Bottom);
        // roadE.transform.position += Vector3.right * 4f;
        // var roadF = InstantiateLShapeRoad(EnumRoadDirection.Left);
        // roadF.transform.position += (Vector3.right + Vector3.down) * 4f;

        //TShape
        // var roadG = InstantiateTShapeRoad(EnumRoadDirection.Top);
        // var roadH = InstantiateTShapeRoad(EnumRoadDirection.Right);
        // roadH.transform.position += Vector3.down * 4f;
        // var roadI = InstantiateTShapeRoad(EnumRoadDirection.Bottom);
        // roadI.transform.position += Vector3.right * 4f;
        // var roadJ = InstantiateTShapeRoad(EnumRoadDirection.Left);
        // roadJ.transform.position += (Vector3.right + Vector3.down) * 4f;

        
        //Cross
        // var roadH = InstantiateCrossShapeRoad();
        

        // roadB.transform.position = NestSystem.Instance.BuildingSystem.Instance.GetSnappedPosition(roadB);

        // Debug.Log("CanPlaceable: " + NestSystem.Instance.BuildingSystem.Instance.CanPlaceable(roadB));
        // NestSystem.Instance.BuildingSystem.Instance.PlaceElementWithAutoConnect(roadB, 0.1f);


        // NestSystem.Instance.ConnectElements(roadA.GetNodes().First(node => node.Name == "wild_right"), roadB.GetNodes().First(node => node.Name == "wild_left"));



        // Debug.Log("Vertical");
        // var verticalRoadData = new IShapeRoadData(EnumRoadHVDirection.Vertical);
        // var verticalRoad = NestSystem.Instance.InstantiateNestElement(verticalRoadData);
        // verticalRoad.transform.position += Vector3.right * 4;

    }

    private NestElement InstantiateIShapeRoad(EnumRoadHVDirection direction){
        var roadData = new IShapeRoadData(direction); 
        return NestSystem.Instance.InstantiateNestElement(roadData);
    }
    
    private NestElement InstantiateLShapeRoad(EnumRoadDirection direction){
        var roadData = new LShapeRoadData(direction); 
        return NestSystem.Instance.InstantiateNestElement(roadData);
    }
    
    private NestElement InstantiateTShapeRoad(EnumRoadDirection direction){
        var roadData = new TShapeRoadData(direction);
        return NestSystem.Instance.InstantiateNestElement(roadData);
    }
    
    private NestElement InstantiateCrossShapeRoad(){
        var roadData = new CrossShapeRoadData(); 
        return NestSystem.Instance.InstantiateNestElement(roadData);
    }


    // Update is called once per frame
    void Update(){
        
    }
}
