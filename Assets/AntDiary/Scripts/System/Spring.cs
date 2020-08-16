using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AntDiary;

public class Spring : MonoBehaviour
{

    public Text Springtext;
    private int counttime;

    private float season;
    private void Start()
    {
      season=0;
      Springtext.text = "";  
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        season = TimeSystem.Instance.CurrentSeason;
        Springcheck();
    }

    private void Springcheck()
    {
        if(season==3){
            Springtext.text = "Spring";
        }
    } 
}
