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

        public Transform itemHolder;

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

        public Item SelectItem(int slot)
        {
            if (slot < 0 || slot > hotbarSlots) return null;      
            if(currentItem != null) currentItem.gameObject.SetActive(false);

            hotbarSlotsUI[selectedHotbarSlot].transform.GetChild(0).gameObject.SetActive(false);
            selectedHotbarSlot = slot - 1;
            currentItem = hotbar[selectedHotbarSlot];

            if (selectedHotbarSlot < hotbarSlotsUI.Count)
            {
                hotbarSlotsUI[selectedHotbarSlot].transform.GetChild(0).gameObject.SetActive(true);
            }

            if (currentItem != null)
            {
                currentItem.gameObject.SetActive(true);
                PlayerHUD.instance.ChangeItemName(currentItem);
                PutItemIntoHand();
            }
            return currentItem;
        }

        private void PutItemIntoHand()
        {
            if (currentItem != null)
            {
                itemHolder.gameObject.SetActive(true);
                currentItem.transform.SetParent(itemHolder);
                currentItem.transform.localPosition = Vector3.zero;
                currentItem.transform.rotation = new Quaternion(0, 0, 0, 0);
                currentItem.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        public void UseItem()
        {
            if (currentItem == null) return;
            currentItem.Use(_player);
        }

        public void DropItem()
        {
            if(currentItem == null) return;
            currentItem.transform.SetParent(null);
            hotbar[Array.IndexOf(hotbar, currentItem)] = null;
            currentItem.onGround = true;
            currentItem = null;
            UpdateHotbarUI();
        }

        public void RemoveItem(Item item)
        {
            hotbar[Array.IndexOf(hotbar, item)] = null;
            Destroy(item.gameObject);
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
