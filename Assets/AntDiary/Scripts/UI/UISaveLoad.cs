using AntDiary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISaveLoad : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] int viewDataCount = 8;
    public int lastSelction;
    [SerializeField] SESystem sesystem;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < viewDataCount; i++)
        {
            GameObject button = Instantiate(buttonPrefab, content);
            button.GetComponent<UISaveDataButton>().Init(i,this,sesystem);
        }
    }

    public void ClickSave()
    {
        SaveSystem.SaveCurrentSaveUnit(lastSelction);
        Debug.Log("Save"+lastSelction);
    }

    public void ClickLoad()
    {
        SaveSystem.LoadSaveUnitToCurrent(lastSelction);
        Debug.Log("Load"+lastSelction);
    }
}
