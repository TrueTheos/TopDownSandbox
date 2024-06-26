using Assets.Scripts.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.EntityStatistics;

namespace Assets.Scripts.Items.ItemEvents
{
    public class ModifyStatisticsEvent : ItemEvent
    {
        public List<StatisticValue> Statistics;

        public void Invoke(Entity entity)
        {
            entity.stats.ModifyStatistics(Statistics);
        }
    }
}
