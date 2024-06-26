using Assets.Scripts.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;

namespace Assets.Scripts.Items.Weapons
{
    public class Wand : Weapon
    {
        private WandStatistics WandStats => stats as WandStatistics;

        public override void Use(Player player)
        {
            base.Use(player);
            player.stats.ModifyStatistic(EntityStatistics.StatisticType.CurrentMana, -WandStats.ManaCost);
        }

        public override bool CanUse(Player player)
        {
            return player.stats.CurrentMana >= WandStats.ManaCost;
        }
    }
}
