using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WariateUI_Cicle : MonoBehaviour
{
    public Image redBar;//棒グラフの赤いバーを格納、下も同じ
    public Image yellowBar;
    public Image greenBar;

    public Image redCircle;//円グラフの赤い部分のimageを格納、下も同様
    public Image yellowCircle;
    public Image greenCircle;

    public string GraphType;
    // Start is called before the first frame update
    void OnEnable()
    {
        redCircle.fillAmount = redBar.fillAmount;
        yellowCircle.fillAmount = yellowBar.fillAmount + redCircle.fillAmount;
        greenCircle.fillAmount = greenBar.fillAmount + yellowCircle.fillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (GraphType == "Active")
        {
            redCircle.fillAmount = redBar.fillAmount;
            yellowCircle.fillAmount = yellowBar.fillAmount + redCircle.fillAmount;
            greenCircle.fillAmount = greenBar.fillAmount + yellowCircle.fillAmount;
        }
    }
}
