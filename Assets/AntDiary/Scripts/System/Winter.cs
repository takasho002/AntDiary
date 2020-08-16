using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AntDiary;

public class Winter : MonoBehaviour
{

    public Text Wintertext;
    private int counttime;

    private float season;
    private void Start()
    {
      season=0;
      Wintertext.text = "";  
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        season = TimeSystem.Instance.CurrentSeason;
        Wintercheck();
    }

    private void Wintercheck()
    {
        if(season==3){
            Wintertext.text = "Winter";
        }
    } 
}
