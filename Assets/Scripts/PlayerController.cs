using Assets.Scripts;
using Assets.Scripts.Items.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Theos.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private float _currentMovementSpeed;

        public Transform hand;

        public LayerMask interactableLayer;

        [Header("Controlls")]
        public KeyCode runKey;
        public KeyCode useItemKey;
        public KeyCode interactKey;

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;
        private PlayerAudioManager _playerAudioManager;
        private PlayerStatistics _playerStats => _player.playerStats;
        private InventoryManager _inventory;

        private Rigidbody2D _rb;
        private Camera _cam;
        private Vector2 _moveInput;

        private bool _isRunning;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cam = GetComponentInChildren<Camera>();
            _playerAudioManager = GetComponent<PlayerAudioManager>();
            _inventory = GetComponent<InventoryManager>();
        }

        private void Update()
        {
            if (Input.GetKey(runKey))
            {
                _isRunning = true;
            }
            else
            {
                _isRunning = false;
            }

            if(Input.GetKeyDown(useItemKey))
            {
                _inventory.UseItem();
            }

            for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    int numberPressed = key - KeyCode.Alpha0;
                    Item item = _inventory.SelectItem(numberPressed);        
                }
            }

            if(Input.GetMouseButton(0))
            {
                if(_inventory.currentItem is Bow bow)
                {
                    bow.Pull();
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                if (_inventory.currentItem is Bow bow)
                {
                    bow.Shoot();
                }
            }

            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            hand.transform.rotation = Quaternion.Euler(0, 0, rotZ);

            Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, _playerStats.PickupRange, interactableLayer);

            if (interactables.Length > 0)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    var closest = interactables.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).FirstOrDefault();

                    if (closest != null && closest.TryGetComponent(out IInteractable interactable))
                    {
                        interactable.Interact(_player);
                    }
                }
            }

            UpdateMovement();
            Animate();
        } 

        private void UpdateMovement()
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");

            _currentMovementSpeed = Mathf.Clamp(_moveInput.magnitude, 0f, 1f);

            _moveInput.Normalize();
            _rb.velocity = _moveInput * _currentMovementSpeed * _playerStats.GetSpeed(_isRunning);

            if (_currentMovementSpeed > 0f) _playerAudioManager.UpdateStep(Time.deltaTime * _playerStats.GetSpeed(_isRunning));
        }

        private void Animate()
        {
            if (_moveInput != Vector2.zero)
            {
                _animator.SetFloat("Horizontal", _moveInput.x);
                _animator.SetFloat("Vertical", _moveInput.y);
            }

            if(_currentMovementSpeed > 0) 
            {
                if(_isRunning) _animator.SetFloat("Speed", 2);
                else _animator.SetFloat("Speed", 1);
            } 
            else _animator.SetFloat("Speed", 0);
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _playerStats.PickupRange);
        }
    }
}
