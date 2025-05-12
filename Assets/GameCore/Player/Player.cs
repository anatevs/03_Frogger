using GameManagement;
using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Player : MonoBehaviour,
        IEndRoundListener
    {
        public event Action<Transform> OnWin;

        public event Action OnDropped;

        public event Action OnCarHit;

        [SerializeField]
        private Transform _startPos;

        [SerializeField]
        private LayerMask _logsLayer;

        [SerializeField]
        private LayerMask _carsLayer;

        [SerializeField]
        private PlayerJump _playerJump;

        private LayerMask _waterLayer = 1 << 4;

        private bool _isOnLog;

        private Transform _currentLog;

        private float _currentLogShiftX;

        public void EndRound()
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

            if ((collisionLayer & _logsLayer.value) > 0)
            {
                _isOnLog = true;

                _currentLog = collision.transform;

                _currentLogShiftX = collision.transform.position.x - transform.position.x;
            }

            if ((collisionLayer & _carsLayer.value) > 0)
            {
                Debug.Log("car collision");

                return;
            }

            if ((collisionLayer & _waterLayer) > 0)
            {
                Debug.Log("water collision");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _logsLayer.value) > 0)
            {
                _isOnLog = false;

                _currentLog = null;
            }
        }
    }
}