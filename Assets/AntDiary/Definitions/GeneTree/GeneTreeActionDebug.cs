using System.Collections;
using System.Collections.Generic;
using AntDiary;
using UnityEngine;

public class GeneTreeActionDebug : GeneTreeAction
{
    public override bool Release(string geneId)
    {
        Debug.Log($"GeneTreeActionDebug: Releasing gene (id: \"{geneId}\") requested.");
        return true;
    }
}
