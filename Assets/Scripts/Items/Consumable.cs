using Assets.Scripts.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Consumable : Item
    {
        protected ConsumableStatistics _consumableStats => stats as ConsumableStatistics;

        public override void Use(Player player)
        {
            base.Use(player);
            StartCoroutine(ConsumeAnimation(player));
        }

        private IEnumerator ConsumeAnimation(Player player)
        {
            player.itemUseHolder.sprite = _consumableStats.icon;
            yield return new WaitForSeconds(_consumableStats.consumeTime);
            player.itemUseHolder.sprite = null;
            Consume(player);
        }

        public virtual void Consume(Player player) 
        {
            currentStack--;

            if(currentStack == 0)
            {
                player.inventoryManager.RemoveItem(this);
            }
        }
    }
}
