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
        private LayerMask _floatingLayerMask;

        [SerializeField]
        private LayerMask _damageLayerMask;

        [SerializeField]
        private LayerMask _wallLayer;

        private bool _isOnFloating;

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

            _isOnFloating = false;
        }

        private void Awake()
        {
            _playerJump = GetComponent<PlayerJump>();
        }

        private void Update()
        {
            if (_isOnFloating)
            {
                MoveAfterFloating();
            }
        }

        private void MoveAfterFloating()
        {
            transform.position = new Vector3(
                _currentLog.position.x - _currentLogShiftX,
                transform.position.y,
                transform.position.z);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _floatingLayerMask.value) > 0)
            {
                _isOnFloating = true;

                _currentLog = collision.transform;

                _currentLogShiftX = collision.transform.position.x - transform.position.x;

                return;
            }

            if (IsDamageCollision(collisionLayer))
            {
                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var collisionLayer = 1 << other.gameObject.layer;

            if ((collisionLayer & _wallLayer) > 0)
            {
                Debug.Log("collision with wall!");

                return;
            }

            if (IsDamageCollision(collisionLayer))
            {
                return;
            }
        }

        private bool IsDamageCollision(int collisionLayer)
        {
            if ((collisionLayer & _damageLayerMask.value) > 0)
            {
                if (!_isDamageCollided)
                {
                    OnDamaged?.Invoke();

                    _isDamageCollided = true;
                }

                return true;
            }

            return false;
        }

        private void OnCollisionExit(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _floatingLayerMask.value) > 0)
            {
                _isOnFloating = false;

                _currentLog = null;
            }
        }
    }
}