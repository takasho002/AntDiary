using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AntDiary
{
    public class WariateUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //割り当てUIのグラフィック関連
        //public Transform parentTransform;
        //このスクリプトは持ち手のImageにつけてください
        //ここでは持ち手が動いた時に長さが変わる色の部分をバーと呼ぶことにします
        public Image scrollbar;//持ち手によって直接値が変更されるバーを格納
        public Image grayScrollbar;//scrollbarの真横にある灰色のバーを格納してください
        public Image otherScrollbar1;//持ち手が管理するバー以外の二つのバーの中の色のついたバーを格納(例えばred_barをscrollbarの中に入れたならこの変数にはyellow_barかgreen_barを格納)
        public Image otherScrollbar2;//上に同じ
        public float Max;//持ち手が右へ動かせる上限

        public Text AntJobNum;
        public Text AntTotal;

        //割り当てUIのシステム関連
        //仕事の型リスト
        private List<Type> antjobs;
        private int jobCount;
        public int total;
        public float fillAmount=0.0f;

        public string JobName;
        public int NumOfBar;
        public float Rate=0.0f;

        private NestData nestdata => NestSystem.Instance?.Data;
        Dictionary<Type, int> antCounter = new Dictionary<Type, int>();

        public void OnEnable()
        {
            ////AntDataのサブクラスから仕事をtypeリストとして取得
            antjobs = System.Reflection.Assembly.GetAssembly(typeof(AntData)).GetTypes().Where(x => x.IsSubclassOf(typeof(AntData))).ToList();
            //DebugAntDataは削除
            for (int i = 0; i < antjobs.Count; i++)
            {
                if (antjobs[i].Name.Equals("DebugAntData")|| antjobs[i].Name.Equals("QueenAntData"))
                {
                    antjobs.RemoveAt(i);
                    break;
                }
            }
            jobCount = antjobs.Count;

            antCounter.Clear();
            //antjobsを元にantCounter設定
            for (int i = 0; i < jobCount; i++)
            {
                antCounter.Add(antjobs[i], 0);
            }
            //生きているアリの総数と仕事ごとの数をカウント
            total = 0;
            foreach (var ant in nestdata.Ants)
            {
                Type antjob = ant.GetType();
                Debug.Log(antjob);
                if (antCounter.ContainsKey(antjob) && ant.IsAlive)
                {
                    antCounter[antjob]++;
                    total++;
                }
            }
            if (total == 0) total++;
            if (JobName == "Architecture")
            {

                fillAmount = (float)antCounter[typeof(BuilderAntData)]/total;//本来は建築アリのtypeを格納
                //otherScrollbar1.fillAmount = antCounter[typeof(ErgateAntData)] / total;//本来は建築アリ以外のtype(防衛アリでも働きアリでもどっちでもいい)を格納
                //otherScrollbar2.fillAmount = antCounter[typeof(UnemployedAntData)] / total;//上に同じ
            }
            else if (JobName == "Work")
            {
                fillAmount = (float)antCounter[typeof(ErgateAntData)] / total;
                //otherScrollbar1.fillAmount = (float)antCounter[typeof(BuilderAntData)] / total;
                //otherScrollbar2.fillAmount = (float)antCounter[typeof(UnemployedAntData)] / total;
            }
            else if (JobName == "Deffence")
            {
                fillAmount = (float)antCounter[typeof(UnemployedAntData)] / total;
                //otherScrollbar1.fillAmount = (float)antCounter[typeof(BuilderAntData)] / total;
                //otherScrollbar2.fillAmount = (float)antCounter[typeof(ErgateAntData)] / total;
            }
            else
            {

            }

        }

        public void Update()
        {


            if (transform.localPosition.x > Max)
            {
                transform.localPosition = new Vector3(Max, transform.localPosition.y, transform.localPosition.z);
            }
            else if (transform.localPosition.x < -150)
            {
                transform.localPosition = new Vector3(-150, transform.localPosition.y, transform.localPosition.z);
            }
            scrollbar.fillAmount = (transform.localPosition.x + 150) / 300;

            grayScrollbar.fillAmount = otherScrollbar1.fillAmount + otherScrollbar2.fillAmount;
            Max = (1 - grayScrollbar.fillAmount) * 300 - 150;


            if (JobName == "Architecture")
            {
                AntJobNum.text = "建築:" + Rate +"%";
            }
            else if (JobName == "Work")
            {
                AntJobNum.text = "働き:" + Rate + "%";
            }
            else if (JobName == "Deffence")
            {
                AntJobNum.text = "防衛:" + Rate + "%";
            }
            AntTotal.text = "総数:" + total + "匹";
        }
        public void OnBeginDrag(PointerEventData data)//ドラッグはじめ
        {
        }
        public void OnDrag(PointerEventData data)//ドラッグ中
        {
            transform.position = new Vector3(data.position.x, transform.position.y, transform.position.z);
            //NumOfBar = (int)(scrollbar.fillAmount * total);
            Rate = scrollbar.fillAmount * 100;
        }
        public void OnEndDrag(PointerEventData data)//ドラッグ終わり
        {
            transform.localPosition = new Vector3(scrollbar.fillAmount * 300-150, 0, 0);
        }
    }
}