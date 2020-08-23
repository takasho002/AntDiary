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
        Debug.Log("Horizontal");
        var roadA = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        var roadB = InstantiateIShapeRoad(EnumRoadHVDirection.Horizontal);
        roadB.transform.position += Vector3.right * 5f;

        Debug.Log("canPlace: " + NestSystem.Instance.BuildingSystem.Instance.CanPlaceable(roadB));
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


    // Update is called once per frame
    void Update(){
        
    }
}
