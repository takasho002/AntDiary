using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class UITitle : MonoBehaviour
{
    public void onClickedNewGame()
    {
        UnityEngine.Debug.Log("NewGame");
    }

    public void onClickedLoadGame()
    {
        UnityEngine.Debug.Log("LoadGame");
    }

    public void onClickedExit()
    {
        UnityEngine.Debug.Log("Exit");
    }
}
