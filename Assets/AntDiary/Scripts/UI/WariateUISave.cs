using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace AntDiary
{
    public class WariateUISave : MonoBehaviour
    {
        private int ArchitectAntNum;
        private int WorkAntNum;
        private int DeffenceAntNum;
        private int TotalAntNum;

        public int FreeAntNum;

        public Image ArchitectBar;
        public Image WorkBar;
        public Image DeffenceBar;

        [SerializeField] private WariateUI Architect;
        [SerializeField] private WariateUI Work;
        [SerializeField] private WariateUI Deffence;
        [SerializeField] private WariateUI Total;

        GameObject JobSystem;
        JobAssignmentSystem jobScript;
        // Start is called before the first frame update
        void Start()
        {
            JobSystem = GameObject.Find("JobAssignmentSystem");
            jobScript = JobSystem.GetComponent<JobAssignmentSystem>();
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
            TotalAntNum = Architect.total;

            FreeAntNum = TotalAntNum - (ArchitectAntNum + WorkAntNum + DeffenceAntNum);

            jobScript.ideal_Architect = ArchitectAntNum;
            jobScript.ideal_Soilder = DeffenceAntNum;
            jobScript.ideal_Mule = WorkAntNum;
            jobScript.ideal_Free = FreeAntNum;

            Debug.Log(ArchitectAntNum+":"+WorkAntNum+":"+DeffenceAntNum);
        }
    }
}
    
