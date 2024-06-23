using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effects
{
    public abstract class StatusEffect
    {
        public StatusEffectType type;

        public float duration;
        public float durationLeft;

        public StatusEffect(StatusEffectType type, float duration)
        {
            this.type = type;
            this.duration = duration;
            this.durationLeft = this.duration;
        }

        public abstract void Handle(Entity entity);
    }

    public class BleedStatusEffect : StatusEffect
    {
        public float value;

        public BleedStatusEffect(float value, StatusEffectType type, float duration) : base(type, duration)
        {
            this.value = value;
        }

        public override void Handle(Entity entity)
        {
            
        }
    }

    public enum StatusEffectType
    {
        Bleeding,
        Stunned
    }
}
