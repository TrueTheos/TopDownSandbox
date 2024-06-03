using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Theos.Player
{
    public class PlayerAudioManager : MonoBehaviour
    {
        public AudioSource audioSource;

        public AudioClip stepClip;
        public Vector2 stepCooldown;

        public void UpdateStep(float time)
        {
            stepCooldown.y += time;

            if (stepCooldown.y >= stepCooldown.x)
            {
                audioSource.PlayOneShot(stepClip);
                stepCooldown.y = 0;
            }
        }
    }
}
