using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class WeaponStatistics : ItemStatistics
    {
        /*public Vector2Int damage;

        public float GetDamage(float luck = 1f)
        {
            float rolledDamage = UnityEngine.Random.Range((float)damage.x, (float)damage.y);
            return Mathf.Lerp(damage.x, damage.y, rolledDamage * luck / 100f);
        }*/
    }
}
