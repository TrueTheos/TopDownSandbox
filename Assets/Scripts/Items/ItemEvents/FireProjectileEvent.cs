using Assets.Scripts.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts.Items.ItemEvents
{
    public class FireProjectileEvent : ItemEvent
    {
        public WeaponProjectile Projectile;
        public float Speed;
        public Transform FirePoint;

        public void Invoke()
        {
            Quaternion rotation = FirePoint.rotation;
 
            WeaponProjectile projectile = Instantiate(Projectile.gameObject, FirePoint.position, rotation).GetComponent<WeaponProjectile>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 difference = mouseWorldPosition - FirePoint.transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            projectile.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            projectile.Shoot(Speed);
            Destroy(projectile, 10f);
        }
    }
}
