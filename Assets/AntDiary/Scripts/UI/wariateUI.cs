using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AntDiary
{
    public class wariateUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //public Transform parentTransform;
        //このスクリプトは持ち手のImageにつけてください
        //ここでは持ち手が動いた時に長さが変わる色の部分をバーと呼ぶことにします
        public Image scrollbar;//持ち手によって直接値が変更されるバーを格納(例えば)
        public Image grayScrollbar;//scrollbarの真横にある灰色のバーを格納してください
        public Image otherScrollbar1;//持ち手が管理するバー以外の二つのバーの中の色のついたバーを格納(例えばred_barをscrollbarの中に入れたならこの変数にはyellow_barかgreen_barを格納)
        public Image otherScrollbar2;//上に同じ
        public float Max;//持ち手が右へ動かせる上限


        void start()
        {
            
        }

        public void Update()
        {
            grayScrollbar.fillAmount = otherScrollbar1.fillAmount + otherScrollbar2.fillAmount;
            Max = (1 - grayScrollbar.fillAmount) * 300 - 150;
            if (transform.localPosition.x > Max)
            {
                transform.localPosition = new Vector3(Max, transform.localPosition.y, transform.localPosition.z);
            }
            else if (transform.localPosition.x < -150)
            {
                transform.localPosition = new Vector3(-150, transform.localPosition.y, transform.localPosition.z);
            }
            scrollbar.fillAmount = (transform.localPosition.x + 150) / 300;
        }
        public void OnBeginDrag(PointerEventData data)//ドラッグはじめ
        {
            Debug.Log("OnBeginDrag");
        }
        public void OnDrag(PointerEventData data)//ドラッグ中
        {
            transform.position = new Vector3(data.position.x, transform.position.y, transform.position.z);
            Debug.Log(transform.localPosition);
        }
        public void OnEndDrag(PointerEventData data)//ドラッグ終わり
        {
            Debug.Log("OnEndDrag");
        }

        public void pushDown()
        {

        }
    }
}