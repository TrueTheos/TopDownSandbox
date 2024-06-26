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
            for (int i = 0; i < buffs.Count; i++)
            {
                Buff buff = buffs[i];
                if (buff == null) continue;
                buff.durationLeft -= Time.deltaTime;
                if (buff.durationLeft <= 0)
                {
                    buff.Remove(_entity);
                    buffs[i] = null;
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
        public List<StatisticValue> statValue;
        public bool isPercentageValue;
    }

    public class Buff
    {
        public float duration;
        public float durationLeft;
        public List<StatisticValue> statValue;
        public bool isPercentage;

        public Buff(BuffStruct buffStruct)
        {
            this.duration = buffStruct.duration;
            this.durationLeft = this.duration;
            this.statValue = buffStruct.statValue;
            this.isPercentage = buffStruct.isPercentageValue;
        }

        public void Add(Entity entity) 
        {
            entity.stats.ModifyStatistics(statValue);
        }

        public void Remove(Entity entity)
        {
            entity.stats.ModifyStatistics(statValue);
        }
    }
}
