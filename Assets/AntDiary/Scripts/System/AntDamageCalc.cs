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
        [Range(0, 10)] private int AttackFrequencyPerSecond = default;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            var currentTimeSecond = TimeSystem.CurrentTime;
            bool isAttackTiming = (int)currentTimeSecond * 10 % AttackFrequencyPerSecond == 0;

            if (isAttackTiming)
            {
                //与ダメ・被ダメ計算
            }
        }
    }
}