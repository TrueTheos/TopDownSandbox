using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class DamageCalculator
    {
        public static float GetDamage(Entity attacker, Entity defender, Damage damage)
        {
            float rolledDmg = UnityEngine.Random.Range(damage.minMax.x, damage.minMax.y);
            float outcome = Mathf.Lerp(damage.minMax.x, damage.minMax.y, rolledDmg * attacker.stats.Luck / 100f);

            if(damage.damageType == DamageType.Physical)
            {
                if(defender.stats.Armor > 0)
                {
                    outcome = outcome / (1 + (defender.stats.Armor / 100));
                }            
            }
            else if (damage.damageType == DamageType.Magical)
            {
                if(defender.stats.MagicResistance > 0)
                {
                    outcome -= outcome * (defender.stats.MagicResistance / 100);
                }
            }

            return outcome;
        }
    }
}
