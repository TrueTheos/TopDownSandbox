using Assets.Scripts.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Items.ItemEvents
{
    public class FireProjectileEvent : ItemEvent
    {
        public WeaponProjectile Projectile;

        public override void Invoke(Entity entity)
        {
            //fire projectile
        }
    }
}
