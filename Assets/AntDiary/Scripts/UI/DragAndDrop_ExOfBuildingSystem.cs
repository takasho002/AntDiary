using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AntDiary.Scripts.Roads;
using UnityEngine;


namespace AntDiary
{
    public class DragAndDrop_ExOfBuildingSystem : MonoBehaviour
    {

        private Vector3 position;
        private Vector3 screenToWorldPointPosition;

        public BuildingSystem Instance = NestSystem.Instance.BuildingSystem.Instance;        

        private NestElement nestelement;
        public void PushDown()
        {
            position = Input.mousePosition;
            position.z = 10f;
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

            NestElementData data = new IShapeRoadData(EnumRoadHVDirection.Vertical);
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
            Debug.Log(Instance.CanPlaceable(nestelement));
            if (Instance.CanPlaceable(nestelement) == false)//建築可能な領域でない
            {
                NestSystem.Instance.RemoveNestElement(nestelement);
                Destroy(nestelement.gameObject);
            }
            else//建築可能
            {
                Instance.PlaceElementWithAutoConnect(nestelement);//NestSystemへ登録
                nestelement.transform.position = Instance.GetSnappedPosition(nestelement);//付近のノードにスナップした座標へ置く      
            }
        }
    }
}
