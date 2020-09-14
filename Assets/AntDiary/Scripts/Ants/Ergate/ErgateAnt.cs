using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AntDiary
{
    public class ErgateAnt : Ant<ErgateAntData,ErgateAntCommonData>
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("ErgateAnt: I was born!");

        }

        // Update is called once per frame
        protected override float MovementSpeed { get; } = 1f;

        private bool pathWayStarted = false;

        public bool IsHoldingFood { get => SelfData.IsHoldingFood; set => SelfData.IsHoldingFood = value; }
        public int Capacity { get => SelfCommonData.Capacity; }

       

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