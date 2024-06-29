using Assets.Scripts.Buffs;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.EntityStatistics;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/Consumable", fileName = "Consumable")]
    public class ConsumableStatistics : ItemStatistics
    {
        public float consumeTime;
        public enum ConsumableType { quick, buff }
        public ConsumableType consumableType;
        public List<StatisticValue> stats;
        public BuffStruct buff;
    }
}
