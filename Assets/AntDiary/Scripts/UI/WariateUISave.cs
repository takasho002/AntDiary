using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityScript.Scripting.Pipeline;

namespace AntDiary
{
    public class WariateUISave : MonoBehaviour
    {
        private float ArchitectAntNum;
        private float WorkAntNum;
        private float DeffenceAntNum;
        private float TotalAntNum;

        public float FreeAntNum;

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

            ArchitectAntNum = Architect.Rate;
            WorkAntNum = Work.Rate;
            DeffenceAntNum = Deffence.Rate;
            TotalAntNum = 100.0f;

            FreeAntNum = TotalAntNum - (ArchitectAntNum + WorkAntNum + DeffenceAntNum);

            jobScript.ideal_Architect = ArchitectAntNum;
            jobScript.ideal_Soilder = DeffenceAntNum;
            jobScript.ideal_Mule = WorkAntNum;
            jobScript.ideal_Free = FreeAntNum;

            //無職リストを持ってきてジョブチェンジ
            var unemployedAnts = NestSystem.Instance.SpawnedAnt.Where(n => n.GetType() == typeof(UnemployedAnt) && n.Data.IsAlive);
            Debug.Log(unemployedAnts.Count());
            for (int i = unemployedAnts.Count()-1; i >= 0 ; i--)
            {
                jobScript.AssignJob(unemployedAnts.ElementAt(i));
            }

            Debug.Log(ArchitectAntNum+":"+WorkAntNum+":"+DeffenceAntNum);
        }
    }
}
    
