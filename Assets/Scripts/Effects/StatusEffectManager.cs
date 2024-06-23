using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class StatusEffectManager : MonoBehaviour
    {
        public List<StatusEffect> effects = new();

        public void Update()
        {
            effects.RemoveAll(effect => effect == null);

            foreach (var effect in effects)
            {
                if (effect == null) continue;
                effect.durationLeft -= Time.deltaTime;

                if(effect.durationLeft <= 0 ) effects.Remove(effect);
            }
        }

        public void AddEffect(StatusEffect effect) 
        {
            if (HasEffect(effect.type)) return;

            effects.Add(effect);
        }

        public bool HasEffect(StatusEffectType type)
        {
            return effects.Any(x => x.type == type);
        }
    }
}
