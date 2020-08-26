using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AntDiary.Scripts.Roads;
using UnityEngine;


namespace AntDiary
{
    public class DragAndDrop_Road : MonoBehaviour
    {


        private Vector3 position;
        private Vector3 screenToWorldPointPosition;

        public string RoadName;

        private NestElement nestelement;
        public void PushDown()
        {
            position = Input.mousePosition;
            position.z = 10f;
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

            NestElementData data;
            if(RoadName == "IShapeVertical")
            {
                data = new IShapeRoadData(EnumRoadHVDirection.Vertical);
            }
            else if(RoadName == "IShapeHorizontal")
            {
                data = new IShapeRoadData(EnumRoadHVDirection.Horizontal);
            }
            else if(RoadName == "LShapeBottom")
            {
                data = new LShapeRoadData(EnumRoadDirection.Bottom);
            }
            else if (RoadName == "LShapeRight")
            {
                data = new LShapeRoadData(EnumRoadDirection.Right);
            }
            else if (RoadName == "LShapeTop")
            {
                data = new LShapeRoadData(EnumRoadDirection.Top);
            }
            else if (RoadName == "LShapeLeft")
            {
                data = new LShapeRoadData(EnumRoadDirection.Left);
            }
            else if (RoadName == "TShapeBottom")
            {
                data = new TShapeRoadData(EnumRoadDirection.Bottom);
            }
            else if (RoadName == "TShapeRight")
            {
                data = new TShapeRoadData(EnumRoadDirection.Right);
            }
            else if (RoadName == "TShapeTop")
            {
                data = new TShapeRoadData(EnumRoadDirection.Top);
            }
            else if (RoadName == "TShapeLeft")
            {
                data = new TShapeRoadData(EnumRoadDirection.Left);
            }
            else
            {
                data = new CrossShapeRoadData();
            }
            nestelement = NestSystem.Instance.InstantiateNestElement(data);
            nestelement.transform.position = screenToWorldPointPosition;
        }

        public void PushDrag()
        {
            position = Input.mousePosition;
            position.z = 10f;
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
            nestelement.transform.position = screenToWorldPointPosition;

        }
        public void PushUp()
        {

            if (BuildingSystem.Instance.IsPlaceable(nestelement) == false)//建築可能な領域でない
            {
                NestSystem.Instance.RemoveNestElement(nestelement);
                Destroy(nestelement.gameObject);
            }
            else//建築可能
            {
                nestelement.transform.position = BuildingSystem.Instance.GetSnappedPosition(nestelement);//付近のノードにスナップした座標へ置く      
                BuildingSystem.Instance.PlaceElementWithAutoConnect(nestelement);//NestSystemへ登録
            }
        }
    }
}

