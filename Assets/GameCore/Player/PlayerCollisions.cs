using GameManagement;
using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class PlayerCollisions : MonoBehaviour,
        IRoundEndListener
    {
        public event Action OnDamaged;

        [SerializeField]
        private Transform _startPos;

        [SerializeField]
        private LayerMask _logsLayerMask;

        [SerializeField]
        private LayerMask _damageLayerMask;

        [SerializeField]
        private LayerMask _wallLayer;

        private bool _isOnLog;

        private Transform _currentLog;

        private float _currentLogShiftX;

        private bool _isDamageCollided;

        private PlayerJump _playerJump;

        public void OnEndRound()
        {
            SetToStart();
        }

        public void SetToStart()
        {
            transform.position = _startPos.position;

            _isDamageCollided = false;
        }

        private void Awake()
        {
            _playerJump = GetComponent<PlayerJump>();
        }

        private void Update()
        {
            if (_isOnLog)
            {
                MoveAfterLog();
            }
        }

        private void MoveAfterLog()
        {
            transform.position = new Vector3(
                _currentLog.position.x - _currentLogShiftX,
                transform.position.y,
                transform.position.z);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _logsLayerMask.value) > 0)
            {
                _isOnLog = true;

                _currentLog = collision.transform;

                _currentLogShiftX = collision.transform.position.x - transform.position.x;

                return;
            }

            if ((collisionLayer & _damageLayerMask.value) > 0)
            {
                if (!_isDamageCollided)
                {
                    OnDamaged?.Invoke();

                    _isDamageCollided = true;
                }

                return;
            }

            if (_isOnLog && (collisionLayer & _wallLayer) > 0)
            {
                Debug.Log("collision with bounds!");

                _isOnLog = false;

                OnDamaged?.Invoke();

                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {






            //todo: all objects as triggers!!!!
            var collisionLayer = 1 << other.gameObject.layer;

            if ((collisionLayer & _damageLayerMask.value) > 0)
            {
                if (!_isDamageCollided)
                {
                    OnDamaged?.Invoke();

                    _isDamageCollided = true;
                }

                return;
            }






            if (other.gameObject.TryGetComponent<WinPlace>(out var winPlace))
            {
                winPlace.OnPlayerTriggered(_playerJump);

                return;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _logsLayerMask.value) > 0)
            {
                _isOnLog = false;

                _currentLog = null;
            }
        }
    }
}