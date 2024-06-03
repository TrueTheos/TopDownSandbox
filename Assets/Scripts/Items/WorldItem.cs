using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WorldItem : MonoBehaviour, IInteractable
    {
        private Item _item;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _item = GetComponent<Item>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_spriteRenderer.sprite == null)
            {
                _spriteRenderer.sprite = _item.stats.icon;
            }
        }

        public void PickUp(Player player)
        {
            if (player.inventoryManager.HasEmptySlot())
            {
                player.inventoryManager.AddItem(_item);
            }

            _spriteRenderer.enabled = false;
        }

        public void Interact(Player player)
        {
            PickUp(player);
            Destroy(this);
        }
    }
}
