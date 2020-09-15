using AntDiary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnemyAnt : AnimatorStrategyAnt<EnemyAnt, EnemyAntData>
{
    protected override void SetAnimation(String nowStrategy)
    {
        //現在のアニメーションを終了
        if (preStrategy.Equals("EnemyMoveStrategy"))
        {
            animator.SetBool("Walking", false);
        }
        else if (preStrategy.Equals("EnemyCombatStrategy"))
        {
            animator.SetBool("Attacking", false);
        }

        //次のアニメーションを再生
        preStrategy = nowStrategy;
        if (preStrategy.Equals("EnemyMoveStrategy"))
        {
            animator.SetBool("Walking", true);
        }
        else if (preStrategy.Equals("EnemyCombatStrategy"))
        {
            animator.SetBool("Attacking", true);
        }
    }
}
