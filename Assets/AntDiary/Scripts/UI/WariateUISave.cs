using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityScript.Scripting.Pipeline;
using UnityEditor;

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
            Architect = ArchitectBar.GetComponent<WariateUI>();
            Work = WorkBar.GetComponent<WariateUI>();
            Deffence = DeffenceBar.GetComponent<WariateUI>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnDisable()
        {
            Architect.Rate = jobScript.ideal_Architect;
            Work.Rate = jobScript.ideal_Mule;
            Deffence.Rate = jobScript.ideal_Soilder;
            Architect.scrollbar.fillAmount = Architect.Rate / 100;
            Work.scrollbar.fillAmount = Work.Rate / 100;
            Deffence.scrollbar.fillAmount = Deffence.Rate / 100;
            Architect.transform.localPosition = new Vector3(Architect.Rate * 3 - 150, Architect.transform.localPosition.y, Architect.transform.localPosition.z);
            Work.transform.localPosition = new Vector3(Work.Rate * 3 - 150, Work.transform.localPosition.y, Work.transform.localPosition.z);
            Deffence.transform.localPosition = new Vector3(Deffence.Rate * 3 - 150, Deffence.transform.localPosition.y, Deffence.transform.localPosition.z);
        }

        public void PushUp()
        {

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
    
