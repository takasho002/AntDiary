using AntDiary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private bool winter = false;
    private int season => TimeSystem.Instance.CurrentSeason;

    // Update is called once per frame
    void Update()
    {
        if (season == 3) winter = true;
        if (winter && season == 0) LoadEnding();
    }

    private void LoadEnding()
    {
        SceneManager.LoadScene("EndingScene");
    }

}
