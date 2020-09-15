using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;


public class FadeOut : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip bgmClip;
    bool IsFadeOut = true;
    double FadeOutSeconds = 3.0;
    double FadeDeltaTime = 0;

    //BGMsystem("拡張子抜き、ファイル名");　で使用

    public void BGMsystem(string bgmName)
    {
        audioSource = GetComponent<AudioSource>();
        fadeout();

        StartCoroutine(DelayMethod((float)FadeOutSeconds, () =>
        {
            cBGM(bgmName);
        }));
    }

    public void fadeout()
    {
        //変数初期化
        IsFadeOut = true;
        FadeOutSeconds = 3.0;
        FadeDeltaTime = 0;
        //1fごとにループするフェード処理
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        FadeDeltaTime += Time.deltaTime;
        if (FadeDeltaTime >= FadeOutSeconds)
        {
            FadeDeltaTime = FadeOutSeconds;
            IsFadeOut = false;
        }
        audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);
        //1フレーム遅らせる
        yield return null;
        //FadeOutSeconds未満の秒数経過で再起に入る
        if (IsFadeOut)
        {
            StartCoroutine("Fade");
        }
    }

    void cBGM(string bgmName)
    {
        if (audioSource.clip?.name == bgmName)
        {
            // すでに再生中なら変えない
            return;
        }
        //Resources内に音源を入れないとResourcesは動かないため、フォルダ構造は要注意IsFadeOut)
        bgmClip = Resources.Load<AudioClip>("BGM/" + bgmName);
        audioSource.clip = bgmClip;
        audioSource.Stop();
        audioSource.volume = 1;
        audioSource.Play();
    }

    IEnumerator DelayMethod(float waitTime, Action action)
    {
        //チェンジ処理をフェードアウトに必要な秒数だけ遅らせる
        yield return new WaitForSeconds(waitTime);
        action();
    }

}