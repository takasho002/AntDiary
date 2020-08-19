using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AntDiary
{
    public class DebugAnt : Ant<DebugAntData>
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("DebugAnt: I was born!");
        }

        // Update is called once per frame
        protected override float MovementSpeed { get; } = 1f;

        private bool pathWayStarted = false;

        protected override void Update()
        {
            base.Update();

            if (!pathWayStarted)
            {
                if (SetRandomDestination()) pathWayStarted = true;
            }
        }

        private bool SetRandomDestination()
        {
            var allNodes = NestSystem.Instance.NestPathNodes;
            if (allNodes.Any())
            {
                var dst = allNodes.ElementAt(Random.Range(0, allNodes.Count()));
                StartForPathNode(dst, () => SetRandomDestination(), () => Debug.Log("Aborted"));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}