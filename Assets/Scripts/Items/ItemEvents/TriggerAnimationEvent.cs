using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.ItemEvents
{
    public class TriggerAnimationEvent : ItemEvent
    {
        public Animator Animator;
        public string Trigger;

        public void Invoke()
        {
            Animator.SetTrigger(Trigger);
        }
    }
}
