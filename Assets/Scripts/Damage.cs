using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Damage
    {
        public Vector2 minMax;
        public DamageType damageType;
        public ElementType delementType;
    }

    public enum DamageType
    {
        Physical,
        Magical
    }

    [Flags]
    public enum ElementType
    {
        None,
        Fire,
        Light,
        Water
    }
}
