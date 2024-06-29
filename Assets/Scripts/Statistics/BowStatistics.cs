using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/Weapons/Bow", fileName = "Bow")]
    public class BowStatistics : WeaponStatistics
    {
        public float maxTensionTime;
        public float maxTensionDamageMultiplier;
    }
}
