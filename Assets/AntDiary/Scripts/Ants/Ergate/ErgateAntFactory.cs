using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class ErgateAntFactory : AntFactory<ErgateAntData>
    {
        [SerializeField] private GameObject ergateAntPrefab = default;
        public override Ant InstantiateAnt(ErgateAntData antData)
        {
            var antObject = Instantiate(ergateAntPrefab, antData.Position, Quaternion.identity);
            var ant = antObject.GetComponent<ErgateAnt>();
            ant.Initialize(antData);
            return ant;
        }
    }
}