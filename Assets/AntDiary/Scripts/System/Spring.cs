using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AntDiary;
using UnityEngine.Audio;

public class Spring : MonoBehaviour
{

    public Text Springtext;
    private float PlayBGMornot;
    private float season;
    private void Start()
    {
      PlayBGMornot = 0;
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
        if(season==0){
            Springtext.text = "Spring";
            if(PlayBGMornot == 0){
               //GameObject.Find("spring_bgm_fixed1.mp3");
                PlayBGMornot = PlayBGMornot+1;
            }
            if(season!=0){
            Springtext.text = "";
            PlayBGMornot = 0;
        }
        }
    } 
}
