using AntDiary.Scripts.Ants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AntDiary;
using AntDiary.Scripts.Ants.SoldierAnt;
using System;
using System.Linq;
using Boo.Lang.Runtime.DynamicDispatching;

/// <summary>
/// StrategyAnt用アニメーション管理のスクリプト
/// 死亡アニメーションは死亡で管理
/// </summary>
public abstract class AnimatorStrategyAnt<T,TData> : MonoBehaviour where T: StrategyAnt<TData> where TData: StrategyAntData
{
    protected String preStrategy = "";
    [SerializeField] protected Animator animator;
    [SerializeField] protected T ant;

    private Vector2 prePos;

    // Start is called before the first frame update
    void Start()
    {
        preStrategy = ant.GetCurrentStrategyName();
        SetAnimation(preStrategy);
        prePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var nowStrategy = ant.GetCurrentStrategyName();
        if (!preStrategy.Equals(nowStrategy)) SetAnimation(nowStrategy);
        //進行方向への回転
        if(prePos != (Vector2)transform.position)
        {
            var v = (Vector2)transform.position - prePos;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg; ;
            var scale = transform.localScale;
            var rotation = transform.eulerAngles;
            if (v.x > 0)
            {
                scale.x = -Math.Abs(scale.x);
                rotation.z = angle;
                
            }
            else
            {
                scale.x = Math.Abs(scale.x);
                rotation.z = angle-180f;
            }
            transform.localScale = scale;
            transform.eulerAngles = rotation;
        }
        prePos = (Vector2)transform.position;
    }


    protected abstract void SetAnimation(String nowStrategy);
}
