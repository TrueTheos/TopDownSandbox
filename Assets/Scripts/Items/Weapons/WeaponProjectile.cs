using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class WeaponProjectile : MonoBehaviour
    {
        private float _speed;

        public void Shoot(float speed)
        {
            _speed = speed;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            /*if(collision.gameObject.TryGetComponent(out Entity entity))
            {
                entity.TakeDamage(_damage);
            }*/
        }
    }
}
