using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    AudioSource audioSource;
    public bool IsFade;
    public double FadeOutSeconds = 1.0;
    bool IsFadeOut = true;
    double FadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent <AudioSource> ();
    }

    void Update()
    {
        if (IsFadeOut)
        {
            FadeDeltaTime += Time.deltaTime ;
            if (FadeDeltaTime >= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                IsFadeOut = false;
            }
            audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);
        }
    }
}