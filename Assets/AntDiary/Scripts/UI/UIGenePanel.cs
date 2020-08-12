using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    public class UIGenePanel : MonoBehaviour
    {
        [SerializeField] private UIGeneTreeView treeView;
        
        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            treeView.Build(GeneSystem.Instance.Trees.First());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}