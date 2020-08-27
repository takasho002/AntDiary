using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject savePanel;
    [SerializeField] GameObject[] buttons;
    [SerializeField] Sprite inactive;
    [SerializeField] Sprite active;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int j = 0; j < panels.Length; j++)
            {
                panels[j].SetActive(false);
                buttons[j].GetComponent<Image>().sprite = inactive;
            }
            savePanel.SetActive(!savePanel.activeSelf);
        }
    }

    // Update is called once per frame
    private void TogglePanelActive(int i)
    {
        if (panels[i].activeSelf)
        {
            panels[i].SetActive(false);
        }else{
            for(int j = 0; j < panels.Length; j++)
            {
                panels[j].SetActive(false);
            }
            panels[i].SetActive(true);
        }
        savePanel.SetActive(false);
    }

    private void ToggleButtonImage(int i)
    {
        if (panels[i].activeSelf)
        {
            buttons[i].GetComponent<Image>().sprite = inactive;
        }
        else
        {
            for (int j = 0; j < buttons.Length; j++)
            {
                buttons[j].GetComponent<Image>().sprite = inactive;
            }
            buttons[i].GetComponent<Image>().sprite = active;
        }
    }

    public void ToggleUIandButton(int i)
    {
        ToggleButtonImage(i);
        TogglePanelActive(i);
    }
}
