using AntDiary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSoldierAnt : AnimatorStrategyAnt<SoldierAnt, SoldierAntData>
{
    protected override void SetAnimation(String nowStrategy)
    {
        //現在のアニメーションを終了
        if (preStrategy.Equals("SoldierChaseStrategy"))
        {
            animator.SetBool("Walking", false);
        }
        else if (preStrategy.Equals("SoldierCombatStrategy"))
        {
            animator.SetBool("Attacking", false);
        }

        //次のアニメーションを再生
        preStrategy = nowStrategy;
        if (preStrategy.Equals("SoldierChaseStrategy"))
        {
            animator.SetBool("Walking", true);
        }
        else if (preStrategy.Equals("SoldierCombatStrategy"))
        {
            animator.SetBool("Attacking", true);
        }
    }
}
