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
    public class Consumable : Item
    {
        protected ConsumableStatistics _consumableStats => stats as ConsumableStatistics;

        public override void Use(Player player)
        {
            player.inventoryManager.IsUsingItem = true;
            base.Use(player);
            StartCoroutine(ConsumeAnimation(player));
        }

        private IEnumerator ConsumeAnimation(Player player)
        {
            if (_consumableStats.consumeTime <= 0)
            {
                Consume(player);
            }
            else
            {
                yield return new WaitForSeconds(_consumableStats.consumeTime);
                Consume(player);
            }
            player.inventoryManager.IsUsingItem = false;
        }

        public virtual void Consume(Player player) 
        {
            currentStack--;

            Debug.Log("Xd");
            switch (_consumableStats.consumableType)
            {
                case ConsumableStatistics.ConsumableType.quick:
                    player.stats.ModifyStatistics(_consumableStats.stats);
                    break;
                case ConsumableStatistics.ConsumableType.buff:
                    player.buffManager.AddBuff(_consumableStats.buff);
                    break;
            }

            if (currentStack <= 0)
            {
                player.inventoryManager.RemoveItem(this);
            }
        }
    }
}
