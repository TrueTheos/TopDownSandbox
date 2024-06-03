using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;

namespace Assets.Scripts.Items
{
    public class Potion : Consumable
    {
        public override void Consume(Player player)
        {
            base.Consume(player);

            PotionStatistics potionStats = _consumableStats as PotionStatistics;
            switch (potionStats.potionType)
            {
                case PotionStatistics.PotionType.quick:
                    player.playerStats.ModifyStatistic(potionStats.statType, potionStats.value);
                    break;
                case PotionStatistics.PotionType.buff:
                    player.buffManager.AddStatBuff(potionStats.statType, potionStats.value, potionStats.duration);
                    break;
            }
        }
    }
}
