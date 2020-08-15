using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;


namespace AntDiary
{
    [MessagePackObject()]
    public class GeneData
    {
        /// <summary>
        /// 有効化された遺伝子のGUID一覧。
        /// </summary>
        [Key(0)] public List<string> ActivatedGenes { get; set; } = new List<string>();
        
        //TODO: スキルポイント的なやつ
    }
}