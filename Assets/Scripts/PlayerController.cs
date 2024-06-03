using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theos.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private float _currentMovementSpeed;

        [Header("Controlls")]
        public KeyCode runKey;
        public KeyCode useItemKey;

        [SerializeField] private Animator _animator;
        private Player _player;
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
            _player = GetComponent<Player>();
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
                    _inventory.SelectItem(numberPressed);
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
    }
}
