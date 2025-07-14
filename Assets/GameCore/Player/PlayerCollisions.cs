using GameManagement;
using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class PlayerCollisions : MonoBehaviour,
        IEndRoundListener
    {
        public event Action OnDamaged;

        [SerializeField]
        private Transform _startPos;

        [SerializeField]
        private LayerMask _logsLayerMask;

        [SerializeField]
        private LayerMask _carsLayerMask;

        [SerializeField]
        private LayerMask _wallLayer;

        private LayerMask _waterLayer = 1 << 4;

        private bool _isOnLog;

        private Transform _currentLog;

        private float _currentLogShiftX;

        public void OnEndRound()
        {
            SetToStart();
        }

        public void SetToStart()
        {
            transform.position = _startPos.position;
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

            if ((collisionLayer & _carsLayerMask.value) > 0)
            {
                Debug.Log("car collision");

                OnDamaged?.Invoke();
            }

            if ((collisionLayer & _waterLayer) > 0)
            {
                Debug.Log($"water collision");

                OnDamaged?.Invoke();
            }

            if (_isOnLog && (collisionLayer & _wallLayer) > 0)
            {
                Debug.Log("collision with bounds!");

                _isOnLog = false;

                OnDamaged?.Invoke();
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