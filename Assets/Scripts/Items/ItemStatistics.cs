using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.PlayerStatistics;

namespace Assets.Scripts.Items
{
    public abstract class ItemStatistics : ScriptableObject
    {
        public string name;
        public string description;
        public Sprite icon;
        public int maxStack = 1;
    }

    public abstract class ConsumableStatistics : ItemStatistics
    {
        public float consumeTime;
    }

    [CreateAssetMenu(menuName = "Items/Potion", fileName = "Potion" )]
    public class PotionStatistics : ConsumableStatistics 
    {
        public enum PotionType { quick, buff}
        public PotionType potionType;
        public PlayerStatsType statType;
        public float value;
        public float duration;
    }
}
