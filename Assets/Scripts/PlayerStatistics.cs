using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName ="PlayerStats", menuName ="EntityStats/Player")]
    public class PlayerStatistics : EntityStatistics
    {
        public float RunSpeedModifier;
        [HideInInspector]public float TotalSpeedModifier = 1;

        public float Luck;

        public float PickupRange;

        public float GetSpeed(bool isSprinting)
        {
            float result = WalkSpeed;
            if (isSprinting) result *= RunSpeedModifier;
            if (TotalSpeedModifier > 1) result *= TotalSpeedModifier;

            return result;
        }

        public void ModifyStatistic(PlayerStatsType statType, float value)
        {
            switch (statType)
            {
                case PlayerStatsType.MaxHealth:
                    break;
                case PlayerStatsType.CurrentHealth:
                    break;
                case PlayerStatsType.WalkSpeed:
                    break;
                case PlayerStatsType.RunSpeed:
                    break;
                case PlayerStatsType.TotalSpeedModifier:
                    TotalSpeedModifier += value;
                    break;
            }
        }

        public enum PlayerStatsType
        {
            MaxHealth,
            CurrentHealth,
            WalkSpeed,
            RunSpeed,
            TotalSpeedModifier
        }
    }
}