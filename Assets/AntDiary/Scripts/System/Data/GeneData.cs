using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;


namespace AntDiary
{
    [MessagePackObject()]
    public class GeneData : MonoBehaviour
    {
        /// <summary>
        /// 有効化された遺伝子のID一覧。
        /// </summary>
        [Key(0)] public List<string> ActivatedGenes { get; set; } = new List<string>();
    }
}