using Assets.Scripts;
using Assets.Scripts.Buffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theos.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerAudioManager))]
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(BuffManager))]
    public class Player : Entity
    {
        [HideInInspector] public PlayerStatistics playerStats => stats as PlayerStatistics;
        public SpriteRenderer itemUseHolder;
        [HideInInspector] public InventoryManager inventoryManager;
        [HideInInspector] public BuffManager buffManager;

        public override void Awake()
        {
            base.Awake();
            buffManager = GetComponent<BuffManager>();
            inventoryManager = GetComponent<InventoryManager>();
        }
    }
}
