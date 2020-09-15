using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class AntDamageCalc : MonoBehaviour
    {
        [SerializeField] Ant myself = default;

        void Start()
        {
            myself = GetComponent<Ant>();
        }

        /// <summary>
        /// 敵アリのHPを減算するメソッド．
        /// 自分のAntクラスを登録しておく必要があります．
        /// </summary>
        /// <param name="enemy">敵アリオブジェクトを指定</param>
        void ReduceEnemyHP(Ant enemy)
        {
            enemy.Data.Health -= 1/*myself.Data.Attack*/; ;
        }
    }
}