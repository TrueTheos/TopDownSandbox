using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IInteractable
    {
        public bool CanInteract();
        public void Interact(Player player);
        public GameObject GetGameObject();
    }
}
