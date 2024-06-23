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

        [Header("Base Stats")]
        public float MaxHealth;
        [HideInInspector] public float CurrentHealh;
        public float WalkSpeed;
        public float RunSpeedModifier;
        [HideInInspector] public float TotalSpeedModifier = 1;
        public float Vigor = 10; // HP
        public float Intelligence = 10; //Mana / SpellPower
        public float Dexterity = 10; //Stamina
        public float Strength = 10; //Attack power, pozwala nosic ciezki armor?
        public float PickupRange;
        public float Luck; // Crit chance
        [HideInInspector] public float StaminaRechargeRate = 20; // ile % z max staminy recharguje w 1 sekunde
        public float StartRecharginStaminaCooldown = 2;

        [HideInInspector] public float MaxStamina => GetStamina();
        [HideInInspector] public float CurrentStamina;

        public void Init()
        {
            CurrentHealh = MaxHealth;
            CurrentStamina = MaxStamina;
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
                    MaxHealth += value;
                    break;
                case StatisticType.CurrentHealth:
                    CurrentHealh += value;
                    break;
                case StatisticType.WalkSpeed:
                    WalkSpeed += value;
                    break;
                case StatisticType.RunSpeed:
                    RunSpeedModifier += value;
                    break;
                case StatisticType.TotalSpeedModifier:
                    TotalSpeedModifier += value;
                    break;
                case StatisticType.Vigor:
                    Vigor += value;
                    break;
                case StatisticType.Intelligence:
                    Intelligence += value;
                    break;
                case StatisticType.Dexterity:
                    Dexterity += value;
                    break;
                case StatisticType.Strength:
                    Strength += value;
                    break;
                case StatisticType.Luck:
                    Luck += value;
                    break;
                case StatisticType.PickupRange:
                    PickupRange += value;
                    break;
            }
        }

        public float GetStamina()
        {
            return Dexterity; //todo jakas formuła na stamine
        }

        public enum StatisticType
        {
            MaxHealth,
            CurrentHealth,
            WalkSpeed,
            RunSpeed,
            TotalSpeedModifier,
            Vigor,
            Intelligence,
            Dexterity,
            Strength,
            Luck,
            PickupRange
        }
    }
}
