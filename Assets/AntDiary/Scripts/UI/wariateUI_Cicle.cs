using AntDiary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WariateUI_Cicle : MonoBehaviour
{
    public WariateUI redBar;//棒グラフの赤いバーを格納、下も同じ
    public WariateUI yellowBar;
    public WariateUI greenBar;

    public Image redCircle;//円グラフの赤い部分のimageを格納、下も同様
    public Image yellowCircle;
    public Image greenCircle;

    public string GraphType;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GraphType == "Active")
        {
            redCircle.fillAmount = redBar.scrollbar.fillAmount;
            yellowCircle.fillAmount = yellowBar.scrollbar.fillAmount + redCircle.fillAmount;
            greenCircle.fillAmount = greenBar.scrollbar.fillAmount + yellowCircle.fillAmount;
        }
        else
        {
            redCircle.fillAmount = redBar.fillAmount;
            yellowCircle.fillAmount = yellowBar.fillAmount + redCircle.fillAmount;
            greenCircle.fillAmount = greenBar.fillAmount + yellowCircle.fillAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GraphType == "Active")
        {
            redCircle.fillAmount = redBar.scrollbar.fillAmount;
            yellowCircle.fillAmount = yellowBar.scrollbar.fillAmount + redCircle.fillAmount;
            greenCircle.fillAmount = greenBar.scrollbar.fillAmount + yellowCircle.fillAmount;
        }
    }
}
