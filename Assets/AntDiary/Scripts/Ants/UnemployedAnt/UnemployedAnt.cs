using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{ 
    public class UnemployedAnt : Ant<UnemployedAntData>
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("UnemployedAnt spawned");
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        // DebugAntそのまま
        protected override float MovementSpeed { get; } = 1f;
    }
}