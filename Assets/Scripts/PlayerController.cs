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
        public KeyCode dropItemKey;

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;
        private PlayerAudioManager _playerAudioManager;
        private EntityStatistics _playerStats => _player.stats;
        private InventoryManager _inventory;

        private Rigidbody2D _rb;
        private Camera _cam;
        private Vector2 _moveInput;

        private bool _isRunning;

        private PlayerHUD _playerHUD;

        private Coroutine _rechargeCoroutine;
        private Coroutine _hideStaminaBarCoroutine;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cam = GetComponentInChildren<Camera>();
            _playerAudioManager = GetComponent<PlayerAudioManager>();
            _inventory = GetComponent<InventoryManager>();
        }

        private void Start()
        {
            _playerHUD = PlayerHUD.instance;
        }

        private IEnumerator RechargeStamina()
        {
            yield return new WaitForSeconds(_playerStats.StartRecharginStaminaCooldown);

            while (_playerStats.CurrentStamina < _playerStats.MaxStamina && !_isRunning)
            {
                _playerStats.CurrentStamina += (_playerStats.MaxStamina * (_playerStats.StaminaRechargeRate/ 100f)) * Time.deltaTime;
                if (_playerStats.CurrentStamina > _playerStats.MaxStamina)
                {
                    _playerStats.CurrentStamina = _playerStats.MaxStamina;
                }
                yield return null;
            }
            _rechargeCoroutine = null;

            _hideStaminaBarCoroutine = StartCoroutine(DelayedHideStaminaBar(2f));
        }

        private IEnumerator DelayedHideStaminaBar(float delay)
        {
            yield return new WaitForSeconds(delay);

            if(_playerStats.CurrentStamina == _playerStats.MaxStamina)
            {
                _playerHUD.ToggleStaminaBar(false);
            }
        }

        private void Update()
        {
            if (Input.GetKey(runKey) && _playerStats.CurrentStamina > 0f)
            {
                _playerStats.CurrentStamina -= Time.deltaTime;
                if (_playerStats.CurrentStamina < 0)
                {
                    _playerStats.CurrentStamina = 0;
                }
               _isRunning = true;

                if (_rechargeCoroutine != null)
                {
                    StopCoroutine(_rechargeCoroutine);
                    _rechargeCoroutine = null;
                }

                _playerHUD.ToggleStaminaBar(true);

                if (_hideStaminaBarCoroutine != null)
                {
                    StopCoroutine(_hideStaminaBarCoroutine);
                    _hideStaminaBarCoroutine = null;
                }
            }
            else
            {
                if (_isRunning)
                {
                    _isRunning = false;
                    _rechargeCoroutine ??= StartCoroutine(RechargeStamina());
                }
            }

            if(Input.GetKeyDown(useItemKey))
            {
                _inventory.UseItem();
            }

            if(Input.GetKeyDown(dropItemKey))
            {
                _inventory.DropItem();
            }

            for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    int numberPressed = key - KeyCode.Alpha0;
                    if(!_inventory.IsUsingItem) _inventory.SelectItem(numberPressed);        
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

            List<IInteractable> interactables = Physics2D.OverlapCircleAll(transform.position, _playerStats.PickupRange, interactableLayer).Select(x => x.GetComponent<IInteractable>()).ToList();
            interactables = interactables.Where(x => x.CanInteract()).ToList();

            if (interactables.Count > 0)
            {
                _playerHUD.TogglePickupHint(true);

                if (Input.GetKeyDown(interactKey))
                {
                    var closest = interactables.OrderBy(x => Vector2.Distance(transform.position, x.GetGameObject().transform.position)).FirstOrDefault();

                    if (closest != null)
                    {
                        closest.Interact(_player);
                    }
                }
            }
            else
            {
                _playerHUD.TogglePickupHint(false);
            }

            _playerHUD.UpdateStaminaBar(_playerStats.CurrentStamina, _playerStats.MaxStamina);
            UpdateMovement();
            Animate();
            UpdateHand();
        } 

        private void UpdateHand()
        {
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            /*Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            hand.transform.rotation = Quaternion.Euler(0, 0, rotZ);*/

            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            hand.transform.right = direction;

            Vector2 scale = hand.transform.localScale;
            if(direction.x < 0)
            {
                scale.y = -1;
            }
            else if(direction.x > 0)
            {
                scale.y = 1;
            }
            hand.transform.localScale = scale;

            if(hand.transform.eulerAngles.z > 0 && hand.transform.eulerAngles.z < 180)
            {
                hand.GetComponentInChildren<SpriteRenderer>().sortingOrder = _player.playerSprite.sortingOrder - 1;
            }
            else
            {
                hand.GetComponentInChildren<SpriteRenderer>().sortingOrder = _player.playerSprite.sortingOrder + 1;
            }
        }

        private void UpdateMovement()
        {
            if (!_player.CanMove()) return;

            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");

            _currentMovementSpeed = Mathf.Clamp(_moveInput.magnitude, 0f, 1f);

            _moveInput.Normalize();
            _rb.velocity = _moveInput * _currentMovementSpeed * _playerStats.GetSpeed(_isRunning);

            if (_currentMovementSpeed > 0f) _playerAudioManager.UpdateStep(Time.deltaTime * _playerStats.GetSpeed(_isRunning));
        }

        private void Animate()
        {
            Vector2 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = transform.position;
            Vector2 directionToMouse = (mousePosition - playerPosition).normalized;
            _animator.SetFloat("Horizontal", directionToMouse.x);
            _animator.SetFloat("Vertical", directionToMouse.y);

            if (_currentMovementSpeed > 0) 
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
