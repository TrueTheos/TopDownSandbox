using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Items.ItemEvents
{
    public class TimedEvent : ItemEvent
    {
        public float time;
        public UnityEvent timedEvent;

        public void Start()
        {
            StartCoroutine(WaitForEvent(time));
        }

        private IEnumerator WaitForEvent(float wait)
        {
            yield return new WaitForSeconds(wait);
            timedEvent.Invoke();
        }
    }
}
