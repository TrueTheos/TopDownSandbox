using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DelayedDestroy : MonoBehaviour
    {
        public float Delay;

        private void Start()
        {
            Destroy(gameObject, Delay);
        }
    }
}
