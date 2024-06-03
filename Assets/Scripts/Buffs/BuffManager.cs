using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;
using static Assets.Scripts.PlayerStatistics;

namespace Assets.Scripts.Buffs
{
    public class BuffManager : MonoBehaviour
    {
        public List<Buff> buffs = new();

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            foreach (Buff buff in buffs)
            {
                if (buff == null) continue;
                buff.durationLeft -= Time.deltaTime;
                if (buff.durationLeft <= 0)
                {
                    buff.Remove(_player);
                    buffs.Remove(buff);
                }
            }

            buffs = buffs.Where(x => x != null).ToList();
        }

        public void AddStatBuff(PlayerStatsType statType, float value, float duration)
        {
            StatBuff buff = new StatBuff(statType, value, duration);
            buff.Add(_player);
            buffs.Add(new StatBuff(statType, value, duration));            
        }
    }

    public abstract class Buff
    {
        public float duration;
        public float durationLeft;

        public Buff(float duration)
        {
            this.duration = duration;
            this.durationLeft = this.duration;
        }

        public virtual void Add(Player player) { }

        public virtual void Remove(Player player) { }
    }

    public class StatBuff : Buff 
    {
        public PlayerStatsType statType;
        public float value;

        public StatBuff(PlayerStatsType statType, float value, float duration) : base(duration)
        {
            this.statType = statType;
            this.value = value;
        }

        public override void Add(Player player)
        {
            base.Add(player);
            player.playerStats.ModifyStatistic(statType, value);
        }

        public override void Remove(Player player)
        {
            player.playerStats.ModifyStatistic(statType, -value);
            base.Remove(player);
        }
    }
}
