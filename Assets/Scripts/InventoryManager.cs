using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Theos.Player
{
    public class InventoryManager : MonoBehaviour
    {
        public int hotbarSlots;
        public Item[] hotbar;

        public List<Image> hotbarSlotsUI = new();

        public Item currentItem;
        private int selectedHotbarSlot;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();

            if (hotbar == null || hotbar.Length != hotbarSlots)
            {
                hotbar = new Item[hotbarSlots];
            }
        }

        private void Start()
        {
            UpdateHotbarUI();
        }

        private void UpdateHotbarUI()
        {
            for (int i = 0; i < hotbarSlots; i++)
            {
                if (i < hotbarSlotsUI.Count)
                {
                    if (hotbar[i] != null)
                    {
                        hotbarSlotsUI[i].enabled = true;
                        hotbarSlotsUI[i].sprite = hotbar[i].stats.icon;
                    }
                    else
                    {
                        hotbarSlotsUI[i].enabled = false;
                    }
                }
            }
        }

        public bool HasEmptySlot()
        {
            return hotbar.Any(x => x == null);
        }

        public void SelectItem(int slot)
        {
            if (slot < 0 || slot > hotbarSlots) return;
            hotbarSlotsUI[selectedHotbarSlot].transform.GetChild(0).gameObject.SetActive(false);
            selectedHotbarSlot = slot - 1;
            currentItem = hotbar[selectedHotbarSlot];

            if (selectedHotbarSlot < hotbarSlotsUI.Count)
            {
                hotbarSlotsUI[selectedHotbarSlot].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        public void UseItem()
        {
            if (currentItem == null) return;
            currentItem.Use(_player);
        }

        public void RemoveItem(Item item)
        {
            hotbar[Array.IndexOf(hotbar, item)] = null;
            UpdateHotbarUI();
        }

        public void AddItem(Item item)
        {
            for (int i = 0; i < hotbarSlots; i++)
            {
                if (hotbar[i] == null)
                {
                    hotbar[i] = item;
                    UpdateHotbarUI();
                    return;
                }
            }
        }
    }
}
