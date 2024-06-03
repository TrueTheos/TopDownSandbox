using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private EntityStatistics baseStats;
        [HideInInspector] public EntityStatistics stats;

        public virtual void Awake()
        {
            stats = Instantiate(baseStats);
            stats.Init();
        }

        public void TakeDamage(int amount)
        {
            stats.CurrentHealh -= amount;
            if (stats.CurrentHealh <= 0) Die();
        }

        public virtual void Die()
        {
            Debug.Log($"<color=red>{name} died.");
        }
    }
}
