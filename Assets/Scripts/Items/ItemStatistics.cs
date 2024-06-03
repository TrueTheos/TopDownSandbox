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
        public string itemName;
        public string description;
        public Sprite icon;
        public int maxStack = 1;
    }

    public abstract class ConsumableStatistics : ItemStatistics
    {
        public float consumeTime;
    }

    [CreateAssetMenu(menuName = "Items/Potion", fileName = "Potion")]
    public class PotionStatistics : ConsumableStatistics
    {
        public enum PotionType { quick, buff }
        public PotionType potionType;
        public PlayerStatsType statType;
        public float value;
        public float duration;
    }

    public abstract class WeaponStatistics : ItemStatistics
    {
        public Vector2Int damage;

        public float GetDamage(float luck = 1f)
        {
            float rolledDamage = UnityEngine.Random.Range((float)damage.x, (float)damage.y);
            return Mathf.Lerp(damage.x, damage.y, rolledDamage * luck / 100f);
        }
    }

    public class BowStatistics : WeaponStatistics
    {
        public GameObject projectile;

        public float arrowSpeed;

        public float maxTensionTime;
        public float maxTensionDamageMultiplier;
    }
}
