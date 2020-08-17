using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class wariateUI_Cicle : MonoBehaviour
{
    public Image redBar;
    public Image yellowBar;
    public Image greenBar;
    public Image blueBar;

    public Image redCircle;
    public Image yellowCircle;
    public Image greenCircle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        redCircle.fillAmount = redBar.fillAmount;
        yellowCircle.fillAmount = yellowBar.fillAmount+redCircle.fillAmount;
        greenCircle.fillAmount = greenBar.fillAmount+yellowCircle.fillAmount;
    }
}
