using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace AntDiary
{
    public class WariateUISave : MonoBehaviour
    {
        public int ArchitectAntNum;
        public int WorkAntNum;
        public int DeffenceAntNum;

        public Image ArchitectBar;
        public Image WorkBar;
        public Image DeffenceBar;

        private WariateUI Architect;
        private WariateUI Work;
        private WariateUI Deffence;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PushUp()
        {
            Architect = ArchitectBar.GetComponent<WariateUI>();
            Work = WorkBar.GetComponent<WariateUI>();
            Deffence = DeffenceBar.GetComponent<WariateUI>();

            ArchitectAntNum = Architect.NumOfBar;
            WorkAntNum = Work.NumOfBar;
            DeffenceAntNum = Deffence.NumOfBar;

            Debug.Log(ArchitectAntNum+":"+WorkAntNum+":"+DeffenceAntNum);
        }
    }
}
    
