using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AntDiary;
using UnityEngine.Audio;

public class Spring : MonoBehaviour
{

    public Text Springtext;
    private int PlaySpringBGMornot;
    private float season;
    [SerializeField] ChangeBackground changeBackground;
    [SerializeField] FadeOut fadeOut;
    private void Start()
    {
      PlaySpringBGMornot = 0;
      season=0;
      //Springtext.text = "";  
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
            //Springtext.text = "Spring";
            if(PlaySpringBGMornot == 0)
            {
                changeBackground.SetBackground(0);
                fadeOut.BGMsystem("test1");
                PlaySpringBGMornot = 1;
            }
        }else{
            PlaySpringBGMornot = 0;
        }
    } 
}
