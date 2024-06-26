using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Items.ItemEvents
{
    public class AnimationTriggerEvents : MonoBehaviour
    {
        public UnityEvent Events;

        public void AnimationTrigger()
        {
            Events?.Invoke();
        }
    }
}
