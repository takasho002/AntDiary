using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class QueenAntFactory : AntFactory<QueenAntData>
    {
        [SerializeField] private GameObject queenAntPrefab = default;
        public override Ant InstantiateAnt(QueenAntData antData)
        {
            var antObject = Instantiate(queenAntPrefab, antData.Position, Quaternion.identity);
            var ant = antObject.GetComponent<QueenAnt>();
            ant.Initialize(antData);
            return ant;
        }
    }
}