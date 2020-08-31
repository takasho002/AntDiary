using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary { 
    public class UnemployedAntFactory : AntFactory<UnemployedAntData>
    {
        // FIXME: よくわかんないけどDebugAntFactoryを無職アリにしたバージョン
        [SerializeField] private GameObject unemployedAntPrefab = default;
        public override Ant InstantiateAnt(UnemployedAntData unemployedAntData)
        {
            var unemployedAntObject = Instantiate(unemployedAntPrefab, unemployedAntData.Position, Quaternion.identity);
            var unemployedAnt = unemployedAntObject.GetComponent<UnemployedAnt>();
            unemployedAnt.Initialize(unemployedAntData);
            return unemployedAnt;
        }
    }
}