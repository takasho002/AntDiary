using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class MainGeneTreeAction : GeneTreeAction
    {
        private static readonly string k_totalAbility = "total_ability";
        private static readonly string k_efficiency = "efficiency";
        private static readonly string k_health = "health";
        private static readonly string k_speed = "speed";

        private AntCommonDataRegistryData CommonDataRegistry => GameContext.Current.s_NestData.CommonDataRegistry;
            
        public override bool Release(string geneId)
        {
            if(geneId.StartsWith(k_totalAbility)) AddTotalAbility(3);
            else if(geneId.StartsWith(k_efficiency)) AddEfficiency(5);
            else if(geneId.StartsWith(k_health)) AddHealth(5);
            else if(geneId.StartsWith(k_speed)) AddSpeed(5);
            else
            {
                Debug.LogWarning($"指定された遺伝子IDに対する解放アクションは定義されていません: {geneId}");
                return false;
            }

            return true;
        }

        private void AddTotalAbility(int level)
        {
            AddEfficiency(level);
            AddHealth(level);
            AddSpeed(level);
        }

        private void AddEfficiency(int level)
        {
            foreach (var c in CommonDataRegistry.CommonData)
            {
                c.BasicEfficiency += level;
            }
        }

        private void AddHealth(int level)
        {
            foreach (var c in CommonDataRegistry.CommonData)
            {
                c.MaxHealth += level;
            }
        }
        
        private void AddSpeed(int level)
        {
            foreach (var c in CommonDataRegistry.CommonData)
            {
                c.BasicMovementSpeed += level;
            }
        }
    }
}