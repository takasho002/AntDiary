using System.Collections;
using System.Collections.Generic;
using AntDiary;
using AntDiary.Scripts.Roads;
using UnityEngine;

public class TestRoadInstantiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        Debug.Log("Horizontal");
        var horizontalRoadData = new IShapeRoadData(EnumRoadHVDirection.Horizontal);
        var horizontalRoad = NestSystem.Instance.InstantiateNestElement(horizontalRoadData);
        // NestSystem.Instance.InstantiateNestElement(horizontalRoadData);

        
        Debug.Log("Vertical");
        var verticalRoadData = new IShapeRoadData(EnumRoadHVDirection.Vertical);
        var verticalRoad = NestSystem.Instance.InstantiateNestElement(verticalRoadData);
        // verticalRoad.transform.position += Vector3.right * 4;

    }

    // Update is called once per frame
    void Update(){
        
    }
}
