using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private ItemStatistics _baseStats;
        [HideInInspector] public ItemStatistics stats;
        [HideInInspector] public int currentStack;

        private void Awake()
        {
            stats = Instantiate(_baseStats);
            currentStack = stats.maxStack;
        }

        public virtual void Use(Player player) { }

        public void PickUp(Player player)
        {
            if(player.inventoryManager.HasEmptySlot())
            {
                player.inventoryManager.AddItem(this);
            }

            Debug.Log("coś po podniesieniu trzeba zrobic");
        }
    }
}
