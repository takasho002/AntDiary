using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.ComponentModel;

namespace AntDiary
{

    public class AntDeath:MonoBehaviour
    {
        //アリprefabのantクラスをセット
        [SerializeField] private Ant ant = default;
        [SerializeField] private Animator animator = default;

        void Start()
        {
            //ant = GetComponent<Ant>();
            //animator = GetComponent<Animator>();
        }

        void Update()
        {
            //AntDataからHPを読んで0以下か判定
            if (/*ant.Data.HP=<0 && !ant.Data.Dead*/true)
            {
                //HPが0以下ならステータスを死亡にしアニメーション再生
                //ant.Data.Dead = true;
                //animator.SetTrigger("Dead");
            }
        }

        /// <summary>
        /// NestDataから自身のAntDataを削除&ゲームオブジェクト削除
        /// AnimationEventで死亡アニメ終了時に呼び出してほしい
        /// </summary>
        void Despawn()
        {
            if (/*ant.Data.Dead*/true)
            {
                NestSystem.Instance.Data.Ants.Remove(ant.Data);
                Destroy(this.gameObject);
            }
        }
    }
}