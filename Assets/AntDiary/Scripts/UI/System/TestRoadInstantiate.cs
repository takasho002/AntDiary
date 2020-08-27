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
        var roadH = InstantiateCrossShapeRoad();
        

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
