using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class wariateUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public Transform parentTransform;
    public Image scrollbar;
    public Image grayScrollbar;
    public Image otherScrollbar1;
    public Image otherScrollbar2;
    public float Max;
    void start()
    {

    }

    public void Update()
    {
        grayScrollbar.fillAmount = otherScrollbar1.fillAmount + otherScrollbar2.fillAmount;
        Max = (1 - grayScrollbar.fillAmount) * 300-150;
        if (transform.localPosition.x>Max)
        {
            transform.localPosition = new Vector3(Max,transform.localPosition.y,transform.localPosition.z) ;
        }
        else if(transform.localPosition.x<-150)
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
        transform.position = new Vector3(data.position.x,transform.position.y,transform.position.z);
        Debug.Log(transform.localPosition);
    }
    public void OnEndDrag(PointerEventData data)//ドラッグ終わり
    {
        Debug.Log("OnEndDrag");
    }
}