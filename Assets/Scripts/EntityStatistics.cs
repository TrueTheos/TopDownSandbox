using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "EntityStats")]
    public class EntityStatistics : ScriptableObject
    {
        public string Name;

        public float MaxHealth;
        [HideInInspector] public float CurrentHealh;
        public float WalkSpeed;

        public float RunSpeedModifier;
        [HideInInspector] public float TotalSpeedModifier = 1;

        public float Luck;

        public float PickupRange;

        public void Init()
        {
            CurrentHealh = MaxHealth;
        }

        public float GetSpeed(bool isSprinting)
        {
            float result = WalkSpeed;
            if (isSprinting) result *= RunSpeedModifier;
            if (TotalSpeedModifier > 1) result *= TotalSpeedModifier;

            return result;
        }

        public void ModifyStatistic(StatisticType statType, float value)
        {
            switch (statType)
            {
                case StatisticType.MaxHealth:
                    break;
                case StatisticType.CurrentHealth:
                    break;
                case StatisticType.WalkSpeed:
                    break;
                case StatisticType.RunSpeed:
                    break;
                case StatisticType.TotalSpeedModifier:
                    TotalSpeedModifier += value;
                    break;
            }
        }

        public enum StatisticType
        {
            MaxHealth,
            CurrentHealth,
            WalkSpeed,
            RunSpeed,
            TotalSpeedModifier
        }
    }
}
