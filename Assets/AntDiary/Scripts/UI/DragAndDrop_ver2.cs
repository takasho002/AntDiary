using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using AntDiary.Scripts.Roads;
using UnityEngine;


namespace AntDiary
{
    public class DragAndDrop_ver2 : MonoBehaviour
    {


        private Vector3 position;
        private Vector3 screenToWorldPointPosition;

        public string NestName;

        private NestElement nestelement;

        private int Chochikukonum = 0;
        private int QweenRoomNum = 0;


        public void PushDown()
        {
            position = Input.mousePosition;
            position.z = 10f;
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

            var list = NestSystem.Instance.NestElements;
            for(int i=0;i<list.Count;i++)
            {
                if(list[i].gameObject.name == "Chochikubeya(Clone)")
                {
                    Chochikukonum++;
                }
                else if(list[i].gameObject.name == "QweenAntRoom(Clone)")//仮の名前
                {
                    QweenRoomNum++;
                }
            }

            NestElementData data;
            if(NestName == "IShapeVertical")
            {
                data = new IShapeRoadData(EnumRoadHVDirection.Vertical);
            }
            else if(NestName == "IShapeHorizontal")
            {
                data = new IShapeRoadData(EnumRoadHVDirection.Horizontal);
            }
            else if(NestName == "LShapeBottom")
            {
                data = new LShapeRoadData(EnumRoadDirection.Bottom);
            }
            else if (NestName == "LShapeRight")
            {
                data = new LShapeRoadData(EnumRoadDirection.Right);
            }
            else if (NestName == "LShapeTop")
            {
                data = new LShapeRoadData(EnumRoadDirection.Top);
            }
            else if (NestName == "LShapeLeft")
            {
                data = new LShapeRoadData(EnumRoadDirection.Left);
            }
            else if (NestName == "TShapeBottom")
            {
                data = new TShapeRoadData(EnumRoadDirection.Bottom);
            }
            else if (NestName == "TShapeRight")
            {
                data = new TShapeRoadData(EnumRoadDirection.Right);
            }
            else if (NestName == "TShapeTop")
            {
                data = new TShapeRoadData(EnumRoadDirection.Top);
            }
            else if (NestName == "TShapeLeft")
            {
                data = new TShapeRoadData(EnumRoadDirection.Left);
            }
            else if(NestName == "Chochikubeya")
            {
                //data = new ChochikubeyaData();
                data = new CrossShapeRoadData();
            }
            else if(NestName == "QweenAntRoom")
            {
                //data = new QweenAntRoomData();
                data = new CrossShapeRoadData();
            }
            else if(NestName =="Cross")
            {
                data = new CrossShapeRoadData();
            }
            else
            {
                data = new CrossShapeRoadData();
            }

            if ((NestName == "Chochikubeya" && Chochikukonum != 0) || (NestName == "QweenAntRoom" && QweenRoomNum != 0))
            {
                
            }
            else
            {
                nestelement = NestSystem.Instance.InstantiateNestElement(data);
                nestelement.transform.position = screenToWorldPointPosition;
            }
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

