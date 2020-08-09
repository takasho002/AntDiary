using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        void Update()
        {
            
        }
    }
}