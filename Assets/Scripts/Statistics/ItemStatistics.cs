using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class ItemStatistics : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite icon;
        public int maxStack = 1;
    }
}
