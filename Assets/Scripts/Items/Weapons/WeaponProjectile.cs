using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class WeaponProjectile :MonoBehaviour
    {
        private float _damage;

        public void Init(float damage)
        {
            _damage = damage;

            //toodo make it fly baby
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out Entity entity))
            {
                entity.TakeDamage(_damage);
            }
        }
    }
}
