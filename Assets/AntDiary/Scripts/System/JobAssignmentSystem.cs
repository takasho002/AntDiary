using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace AntDiary
{
    /// <summary>
    /// 仕事割り振りクラス
    /// </summary>
    public class JobAssignmentSystem
    {
        //仕事の名前
        private String[] jobs = new string[4] { "Architect", "Soilder", "Mule", "Free" };
        //理想の割合
        public float[] idealrate { get; }

        public JobAssignmentSystem()
        {
            idealrate = new float[4] { 25.0f, 25.0f, 25.0f, 25.0f };
        }

        /// <summary>
        /// 仕事割り振り関数
        /// </summary>
        /// <param name="current_Architect">現在の建築家の数</param>
        /// <param name="current_Soilder">現在の兵士の数</param>
        /// <param name="current_Mule">現在の運搬屋の数</param>
        /// <param name="current_Free">現在の無職の数</param>
        /// <returns>新しく割り振る仕事</returns>
        public string AssignJob(int current_Architect,int current_Soilder, int current_Mule, int current_Free)
        {
            //int[] ideal = new int[4]; //{ ideal_Architect, ideal_Soilder, ideal_Mule, ideal_Free };
            //引数から現在の仕事のアリの数配列と合計を出す
            //引数じゃなくてNestDataからアクセスできるっぽい?NestDataの変更に応じて修正
            //int total = NestSystem.Instance.Data.Ants.Length; //こんな感じ
            int[] current = new int[4] { current_Architect, current_Soilder, current_Mule, current_Free };
            int total = current.Sum();

            //diffに現在と理想の割合の差を保存
            float[] diff = new float[4];
            int[] index = new int[4];

            for (int i = 0; i < idealrate.Length; i++)
            {
                diff[i] = 100.0f * current[i] / total - idealrate[i];
                index[i] = i;
            }

            //diffを元にindexをソート
            Array.Sort(diff,index);
            //一番理想より少ない役職名を返す
            return jobs[index[0]];
        }

        /// <summary>
        /// 理想値更新関数
        /// </summary>
        /// <param name="new_ideal">新しい理想値</param>
        public void UpdateIdealRate(float[] new_ideal)
        {
            if (new_ideal.Length != idealrate.Length || new_ideal.Sum()<99||new_ideal.Sum()>101)
            {
                return;
            }

            for (int i = 0; i < idealrate.Length; i++)
            {
                idealrate[i] = new_ideal[i];
            }
        }
    }
}