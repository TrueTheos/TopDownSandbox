using Assets.Scripts.Items;
using Assets.Scripts.Items.ItemEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theos.Player;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemStatistics _baseStats;
        [HideInInspector] public ItemStatistics stats;
        [HideInInspector] public int currentStack = 1;

        public UnityEvent OnUseEvents;

        private SpriteRenderer _spriteRenderer;

        [HideInInspector] public bool onGround = true;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_spriteRenderer.sprite == null)
            {
                _spriteRenderer.sprite = _baseStats.icon;
            }

            gameObject.tag = "Item";
        }

        private void Start()
        {
            stats = Instantiate(_baseStats);
            currentStack = stats.maxStack;
        }

        public virtual void Use(Player player) 
        {
            OnUseEvents.Invoke();
        }

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

        public virtual bool CanUse(Player player) { return true; }
    }
}
