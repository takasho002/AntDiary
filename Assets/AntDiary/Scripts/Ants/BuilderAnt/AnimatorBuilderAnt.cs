using AntDiary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBuilderAnt : AnimatorStrategyAnt<BuilderAnt,BuilderAntData>
{
    protected override void SetAnimation(String nowStrategy)
    {
        //現在のアニメーションを終了
        if (preStrategy.Equals("MoveStrategy"))
        {
            animator.SetBool("Walking", false);
        }
        else if (preStrategy.Equals("BuildStrategy"))
        {
            animator.SetBool("Building", false);
        }

        //次のアニメーションを再生
        preStrategy = nowStrategy;
        if (preStrategy.Equals("MoveStrategy"))
        {
            animator.SetBool("Walking", true);
        }
        else if (preStrategy.Equals("BuildStrategy"))
        {
            animator.SetBool("Building", true);
        }
    }
}
