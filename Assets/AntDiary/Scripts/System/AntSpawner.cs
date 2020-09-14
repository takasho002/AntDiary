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
        private QueenRoom queenroom;
        private QueenAnt queen;

        // Start is called before the first frame update
        void Start()
        {
            JobAssignmentSystem = GameObject.Find("JobAssignmentSystem").GetComponent<JobAssignmentSystem>();
            queenroom = GetComponent<QueenRoom>();
        }

        // Update is called once per frame
        public void StartSpawn()
        {
            queen = NestSystem.Instance.GetAnt<QueenAnt>();
            if (!queen) return;
            if(!queenroom.IsUnderConstruction && queen != null)StartCoroutine("Spawn");
        }

        // 実際のアリ生成関数
        public IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(SpawnInterval);
                // アリ生成
                for (int i = 0; i < queen.CommonData.BasicEfficiency; i++)
                {
                    UnemployedAntData data = new UnemployedAntData()
                    {
                        Position = transform.position,
                    };
                    var ant = NestSystem.Instance.InstantiateAnt(data);
                    JobAssignmentSystem.AssignJob(ant);
                    lastSpawnedTime = CurrentTime;
                }
            }
        }

        // スポーン間隔(秒)
        [SerializeField] private float SpawnInterval = 60;

        // 長いんで呼びやすく
        private float CurrentTime => TimeSystem.Instance.CurrentTime;
        // lastSpawnedTimeからの経過時間
        private float DeltaTimeFromLastSpawn => CurrentTime - lastSpawnedTime;
    }
}