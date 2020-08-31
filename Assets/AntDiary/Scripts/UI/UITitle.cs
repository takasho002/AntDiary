using AntDiary;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using AntDiary;
using System;

public class UITitle : MonoBehaviour
{
    public void onClickedNewGame()
    {
        UnityEngine.Debug.Log("NewGame");
        //SceneManager.sceneLoaded += DefaultLoad;
        SceneManager.LoadScene("MainScene");
    }

    public void onClickedLoadGame()
    {
        UnityEngine.Debug.Log("LoadGame");
        //SceneManager.sceneLoaded += DefaultLoad;
        SceneManager.LoadScene("MainScene");
    }

    public void onClickedExit()
    {
        UnityEngine.Debug.Log("Exit");
        Exit();
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    private void DefaultLoad(Scene next, LoadSceneMode mode)
    {
        //SaveSystem.Instance.LoadDefaultSaveUnitToCurrent();
    }

    
}
