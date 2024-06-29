using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.EntityStatistics;

namespace Assets.Scripts.Statistics
{
    [CreateAssetMenu(menuName = "Items/Armor", fileName = "Armor")]
    public class ArmorStatistics : ItemStatistics
    {
        public ArmorType armorType;

        public List<StatisticValue> stats;
        public int weight;
    }

    public enum ArmorType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }
}
