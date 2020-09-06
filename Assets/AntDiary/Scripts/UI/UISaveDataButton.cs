using AntDiary;
using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UISaveDataButton : MonoBehaviour,IPointerEnterHandler
{
    private int id;
    private UISaveLoad uisaveload;
    private SESystem sesystem;

    public void Init(int _id, UISaveLoad _uisaveload, SESystem _sesystem)
    {
        id = _id;
        uisaveload = _uisaveload;
        sesystem = _sesystem;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => ChangeData());
        GetComponentInChildren<Text>().text = "セーブ" + (id + 1);
    }

    public void ChangeData()
    {
        uisaveload.lastSelction = id;
        sesystem.PlaySE("click");
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        sesystem.PlaySE("select");
    }
}
