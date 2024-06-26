using Assets.Scripts.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Items.ItemEvents
{
    public class ModifyStatisticsEvent : ItemEvent
    {
        public List<BuffStruct> Statistics;

        public override void Invoke(Entity entity)
        {
            
        }
    }
}
