using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;
using static Assets.Scripts.EntityStatistics;

namespace Assets.Scripts.Buffs
{
    public class BuffManager : MonoBehaviour
    {
        public List<Buff> buffs = new();

        private Entity _entity;

        private void Awake()
        {
            _entity = GetComponent<Player>();
        }

        private void Update()
        {
            foreach (Buff buff in buffs)
            {
                if (buff == null) continue;
                buff.durationLeft -= Time.deltaTime;
                if (buff.durationLeft <= 0)
                {
                    buff.Remove(_entity);
                    buffs.Remove(buff);
                }
            }

            buffs = buffs.Where(x => x != null).ToList();
        }

        public void AddBuff(BuffStruct buffStruct)
        {
            Buff buff = new Buff(buffStruct);
            buff.Add(_entity);
            buffs.Add(buff);            
        }
    }

    [Serializable]
    public struct BuffStruct
    {
        public float duration;
        public StatisticType statType;
        public float value;
        public bool isPercentageValue;
    }

    public class Buff
    {
        public float duration;
        public float durationLeft;
        public StatisticType statType;
        public float value;
        public bool isPercentage;

        public Buff(BuffStruct buffStruct)
        {
            this.duration = buffStruct.duration;
            this.durationLeft = this.duration;
            this.statType = buffStruct.statType;
            this.value = buffStruct.value;
            this.isPercentage = buffStruct.isPercentageValue;
        }

        public void Add(Entity entity) 
        {
            entity.stats.ModifyStatistic(statType, value);
        }

        public void Remove(Entity entity)
        {
            entity.stats.ModifyStatistic(statType, -value);
        }
    }
}
