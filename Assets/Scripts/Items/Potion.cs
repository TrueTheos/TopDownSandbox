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
                    player.stats.ModifyStatistic(potionStats.buff.statType, potionStats.buff.value);
                    break;
                case PotionStatistics.PotionType.buff:
                    player.buffManager.AddBuff(potionStats.buff);
                    break;
            }
        }
    }
}
