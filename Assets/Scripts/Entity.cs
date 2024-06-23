using Assets.Scripts.Buffs;
using Assets.Scripts.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BuffManager))]
    [RequireComponent(typeof(StatusEffectManager))]
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private EntityStatistics baseStats;
        [HideInInspector] public EntityStatistics stats;
        [HideInInspector] public BuffManager buffManager;
        [HideInInspector] public StatusEffectManager statusEffectManager;
        public virtual void Awake()
        {
            buffManager = GetComponent<BuffManager>();
            statusEffectManager = GetComponent<StatusEffectManager>();
            stats = Instantiate(baseStats);
            stats.Init();
        }

        public bool CanMove()
        {
            return !statusEffectManager.HasEffect(StatusEffectType.Stunned);
        }

        public void TakeDamage(float amount)
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
