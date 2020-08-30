using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public abstract class GeneTreeAction : MonoBehaviour
    {
        /// <summary>
        /// 指定したIDの遺伝子を開放する。
        /// なお、セーブデータをロードした際に、すでに解放されている遺伝子のReleaseは呼ばれないため、遺伝子の開放によるステータスの更新等はセーブデータに保存される必要がある
        /// </summary>
        /// <param name="geneId"></param>
        /// <returns>解放に成功したかどうか。falseを返すと解放がキャンセルされる</returns>
        public abstract bool Release(string geneId);
    }
}