using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Items.Weapons
{
    public class WeaponProjectile : MonoBehaviour
    {
        private float _speed;
        public UnityEvent onCollisionEvent;
        public UnityEvent onDestroyEvent;
        public enum DestroyType { AnyCollision, Wall, Entity}
        public DestroyType destroyType;

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
            onCollisionEvent.Invoke();

            switch (destroyType)
            {
                case DestroyType.AnyCollision:
                    onDestroyEvent.Invoke();
                    Destroy(this);
                    break;
                case DestroyType.Entity:
                    if (collision.TryGetComponent(out Entity _))
                    {
                        onDestroyEvent.Invoke();
                        Destroy(this);
                    }
                    break;
                case DestroyType.Wall:
                    if(!collision.TryGetComponent(out Entity _))
                    {
                        onDestroyEvent.Invoke();
                        Destroy(this);
                    }
                    break;
            }
        }
    }
}
