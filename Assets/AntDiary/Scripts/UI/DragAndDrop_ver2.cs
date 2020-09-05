using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using AntDiary.Scripts.Roads;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AntDiary
{
    public class DragAndDrop_ver2 : MonoBehaviour
    {


        private Vector3 position;
        private Vector3 screenToWorldPointPosition;
        
        //ボタンから出したい道の形の名前を保存します。If文だらけのとこの名前です
        public string NestName;
        //ドラッグ&ドロップさせる道や部屋を格納します
        private NestElement nestelement;
        //貯蓄庫や女王の部屋の数を格納します
        private int Chochikukonum = 0;
        private int QweenRoomNum = 0;


        public void PushDown()
        {
            position = Input.mousePosition;
            position.z = 10f;
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
            //Listでシーン内にあるNestElementをすべて取得
            var list = NestSystem.Instance.NestElements;
            //貯蓄庫と女王の部屋の数を数えます
            for(int i=0;i<list.Count;i++)
            {
                if (list[i].GetType().Name == "StoreRoom")
                {
                    Chochikukonum++;
                }
                else if(list[i].GetType().Name == "QueenAntRoom")//仮の名前
                {
                    QweenRoomNum++;
                }
            }

            //出現させるNestElementのデータを保存
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
            else if(NestName == "Chochikubeya" && Chochikukonum == 0)
            {
                //data = new ChochikubeyaData();
                data = new StoreRoomData();
            }
            else if(NestName == "QueenAntRoom" && QweenRoomNum == 0)
            {
                //data = new QweenAntRoomData();
                data = new QueenAntRoomData(){IsUnderConstruction = false };
            }
            else if(NestName =="Cross")
            {
                data = new CrossShapeRoadData();
            }
            else
            {
                data = new CrossShapeRoadData();
            }
            
            //貯蓄庫と女王の部屋が指定されたときシーン内に巣でにそれらの部屋があるなら出せない
            if ((NestName == "Chochikubeya" && Chochikukonum != 0) || (NestName == "QueenAntRoom" && QweenRoomNum != 0))
            {
                GetComponent<EventTrigger>().triggers.Clear();
            }
            else
            {
                nestelement = NestSystem.Instance.InstantiateNestElement(data, false, false);
                nestelement.transform.position = screenToWorldPointPosition;
            }
        }

        public void PushDrag()
        {
            //ドラッグ中
                position = Input.mousePosition;
                position.z = 10f;
                screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
                nestelement.transform.position = screenToWorldPointPosition;
                
                //ドラッグ中にスナップ
                nestelement.transform.position = BuildingSystem.Instance.GetSnappedPosition(nestelement);

        }
        public void PushUp()
        {
/*
              if (BuildingSystem.Instance.IsPlaceable(nestelement) == false)//建築可能な領域でない
              {
                //NestElement削除
                  //NestSystem.Instance.RemoveNestElement(nestelement);
                  Destroy(nestelement.gameObject);
              }
              else//建築可能
              {
                //nestelement.transform.position = BuildingSystem.Instance.GetSnappedPosition(nestelement);//付近のノードにスナップした座標へ置く      
                BuildingSystem.Instance.PlaceElementWithAutoConnect(nestelement);//NestSystemへ登録
              }
*/
              if (!BuildingSystem.Instance.PlaceElementWithAutoConnect(nestelement, false))
              {
                  Destroy(nestelement.gameObject);
              }
        }
    }
}

