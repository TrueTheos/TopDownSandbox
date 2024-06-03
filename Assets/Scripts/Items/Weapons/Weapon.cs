using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Items.Weapons
{
    public abstract class Weapon : Item
    {
        protected WeaponStatistics _weaponStats => stats as WeaponStatistics;
    }
}
