using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;
using static UnityEditor.Progress;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemStatistics _baseStats;
        [HideInInspector] public ItemStatistics stats;
        [HideInInspector] public int currentStack;

        private SpriteRenderer _spriteRenderer;

        public bool onGround = true;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_spriteRenderer.sprite == null)
            {
                _spriteRenderer.sprite = _baseStats.icon;
            }
        }

        private void Start()
        {
            stats = Instantiate(_baseStats);
            currentStack = stats.maxStack;
        }


        public virtual void Use(Player player) { }

        public void PickUp(Player player)
        {
            if (!player.inventoryManager.HasEmptySlot()) return;
            player.inventoryManager.AddItem(this);

            gameObject.SetActive(false);
            onGround = false;
        }

        public void Interact(Player player)
        {
            PickUp(player);
        }

        public bool CanInteract()
        {
            return onGround;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
