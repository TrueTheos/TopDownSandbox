﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.ItemEvents
{
    public abstract class ItemEvent : MonoBehaviour
    {
        public abstract void Invoke(Entity entity);
    }
}
