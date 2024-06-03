using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class EntityStatistics : ScriptableObject
    {
        public float MaxHealth;
        [HideInInspector] public float CurrentHealh;
        public float WalkSpeed;

        public void Init()
        {
            CurrentHealh = MaxHealth;
        }
    }
}
