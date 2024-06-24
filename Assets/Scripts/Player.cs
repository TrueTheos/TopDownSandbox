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
    public class Player : Entity
    {
        public SpriteRenderer itemUseHolder;
        public SpriteRenderer playerSprite;
        [HideInInspector] public InventoryManager inventoryManager;

        public override void Awake()
        {
            base.Awake();
            inventoryManager = GetComponent<InventoryManager>();
        }
    }
}
