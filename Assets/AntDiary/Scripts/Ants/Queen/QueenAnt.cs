using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AntDiary
{
    public class QueenAnt : Ant<QueenAntData,QueenAntCommonData>
    {
        // Start is called before the first frame update
        void Start()
        {   
            Debug.Log("QueenAnt: I was born!");

        }

        // Update is called once per frame
        protected override float MovementSpeed => SelfCommonData.BasicMovementSpeed;

        private bool pathWayStarted = false;

        protected override void Update()
        {
            base.Update();

        }

    }
}