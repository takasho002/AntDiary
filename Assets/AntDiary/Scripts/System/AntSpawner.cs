using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class AntSpawner : MonoBehaviour
    {
        // 最後にスポーンさせた時間
        // これでスポーン間隔を管理
        // 初期値: Start()呼び出し時のゲーム内時刻
        private float lastSpawnedTime;
        [SerializeField] private JobAssignmentSystem JobAssignmentSystem;

        // Start is called before the first frame update
        void Start()
        {
            lastSpawnedTime = CurrentTime;

        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine("Spawn");
        }

        // 実際のアリ生成関数
        IEnumerator Spawn()
        {
            if (DeltaTimeFromLastSpawn >= SpawnInterval)
            {
                // アリ生成
                UnemployedAntData data = new UnemployedAntData()
                {
                    Position = transform.position,
                };
                var ant = NestSystem.Instance.InstantiateAnt(data);
                JobAssignmentSystem.AssignJob(ant);
                lastSpawnedTime = CurrentTime;
            }
            return null;
        }

        // スポーン間隔(秒)
        private float SpawnInterval = 1;

        // 長いんで呼びやすく
        private float CurrentTime => TimeSystem.Instance.CurrentTime;
        // lastSpawnedTimeからの経過時間
        private float DeltaTimeFromLastSpawn => CurrentTime - lastSpawnedTime;
    }
}