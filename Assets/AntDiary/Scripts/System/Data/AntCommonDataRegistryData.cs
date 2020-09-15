using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    
    [MessagePackObject]
    public class AntCommonDataRegistryData
    {
        [Key(0)]
        public List<AntCommonDataBase> CommonData { get; set; } = new List<AntCommonDataBase>()
        {
            //初期セーブデータにおけるステータスを定義（各コンストラクタ内で実装）
            new DebugAntCommonData(),
            new BuilderAntCommonData(),
            new ErgateAntCommonData(),
            new UnemployedAntCommonData(),
            new QueenAntCommonData(),
            new EnemyAntCommonData(),
            new SoldierAntCommonData(),
        };

        public AntCommonData<T> GetCommonDataFor<T>() where T : AntData
        {
            return GetCommonData<AntCommonData<T>>();
        }
        
        public bool TryGetCommonDataFor<T>(out AntCommonData<T> result) where T : AntData
        {
            return TryGetCommonData(out result);
        }
        
        public T GetCommonData<T>() where T : AntCommonDataBase
        {
            return CommonData.OfType<T>().FirstOrDefault();
        }
        
        public bool TryGetCommonData<T>(out T result) where T : AntCommonDataBase
        {
            result = GetCommonData<T>();
            if (result == default) return false;
            return true;
        }
    }
}