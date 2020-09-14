using AntDiary;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackWave : MonoBehaviour
{
    private float current => TimeSystem.Instance.CurrentTime;
    [SerializeField] private float nextwave;
    [SerializeField] private float interval = 180f;

    [SerializeField] private int spawncount = 5;
    [SerializeField] private int SpawnRate = 6;

    [SerializeField] private GameObject waveui;
    [SerializeField] private FadeOut fadeout;
    private string attackbgmname = "ari_attack_master2";
    private string[] seasonbgmname = new string [] { "ari_spring_master2", "ari_summer_master2", "ari_autumn_master4", "ari_winter_master", };

    private Vector3 spawnpos = new Vector3(5, 3, 0);

    private bool waving = false;

    // Start is called before the first frame update
    void Start()
    {
        nextwave = interval * Mathf.Floor(current/interval) + interval;
        if(NestSystem.Instance.GetAnts<EnemyAnt>().Any(n => n.Data.IsAlive))
        {
            waving = true;
            fadeout.BGMsystem(attackbgmname);
            waveui.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckStartWave())StartWave();
        if (CheckEndWave()) EndWave();
    }

    private bool CheckStartWave()
    {
        if (current > nextwave)
        {
            if (UnityEngine.Random.Range(0, SpawnRate) < 1)
            {
                nextwave = interval * Mathf.Floor(current / interval) + interval*2;
                return true;
            }
            else
            {
                nextwave = interval * Mathf.Floor(current / interval) + interval;
                return false;
            }
        }

        return false;
    }

    private void StartWave()
    {
        waving = true;
        fadeout.BGMsystem(attackbgmname);
        waveui.SetActive(true);
        for (int i = 0; i < spawncount; i++)
        {
            EnemyAntData enemydata = new EnemyAntData() { Position = spawnpos};
            NestSystem.Instance.InstantiateAnt(enemydata);
        }
    }

    private bool CheckEndWave()
    {
        if (NestSystem.Instance.GetAnts<EnemyAnt>().Any(n => n.Data.IsAlive)) return false;
        if (waving)
        {
            waving = false;
            return true;
        }
        return false;
    }

    private void EndWave()
    {
        fadeout.BGMsystem(seasonbgmname[TimeSystem.Instance.CurrentSeason%4]);
        waveui.SetActive(false);
    }
}
