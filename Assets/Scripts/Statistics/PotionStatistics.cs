using Assets.Scripts.Buffs;
using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/Potion", fileName = "Potion")]
    public class PotionStatistics : ConsumableStatistics
    {
        public enum PotionType { quick, buff }
        public PotionType potionType;
        public BuffStruct buff;
    } 
}
