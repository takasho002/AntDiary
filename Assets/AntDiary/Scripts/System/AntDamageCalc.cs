using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class AntDamageCalc : MonoBehaviour
    {
        TimeSystem TimeSystem => TimeSystem.Instance;

        [SerializeField] private int HP = default;
        [SerializeField] private int Power = default;
        [SerializeField] private float FindEnemyRadius = default;
        [SerializeField] private float AttackFrequencySecond = default;    // 何秒に1回攻撃するか

        private GameObject nearAnt;
        private bool isBattlePhase;
        private bool isAttackTiming;
        private float currentTimeSecond;

        // Start is called before the first frame update
        void Start()
        {
            isBattlePhase = false;
            isAttackTiming = false;
            nearAnt = serchAnt(gameObject, "enemy");  // 敵アリにタグが付いていると仮定
        }

        // Update is called once per frame
        void Update()
        {
            currentTimeSecond = TimeSystem.CurrentTime;
            isAttackTiming = (int)(currentTimeSecond * 10) % (int)(AttackFrequencySecond * 10) == 0;
            nearAnt = serchAnt(gameObject, "enemy");  // 敵アリにタグが付いていると仮定

            if (isAttackTiming & isBattlePhase)
            {
                // 与ダメ・被ダメ計算
            }
        }

        // 指定されたタグの中で最も近いものを取得
        // 攻撃フラグを制御
        GameObject serchAnt(GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           // 距離用一時変数
            float nearDis = 0;          // 最も近いオブジェクトの距離
            GameObject targetAnt = null; // オブジェクト

            // タグ指定されたオブジェクトを配列で取得する
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                // 自身と取得したオブジェクトの距離を取得
                tmpDis = Vector2.Distance(obs.transform.position, nowObj.transform.position);

                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    targetAnt = obs;
                }

            }

            if (nearDis < FindEnemyRadius)
            {
                isBattlePhase = true;
            }
            else
            {
                isBattlePhase = false;
            }

            return targetAnt;
        }
    }
}