using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class DebugAntFactory : AntFactory<DebugAntData>
    {
        [SerializeField] private GameObject debugAntPrefab = default;
        public override Ant InstantiateAnt(DebugAntData antData)
        {
            var antObject = Instantiate(debugAntPrefab, antData.Position, Quaternion.identity);
            var ant = antObject.GetComponent<DebugAnt>();
            ant.Initialize(antData);
            return ant;
        }
    }
}