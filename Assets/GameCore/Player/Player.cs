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

        public int Lifes { get; set; }

        [SerializeField]
        private Transform _startPos;

        [SerializeField]
        private LayerMask _logsLayer;

        [SerializeField]
        private LayerMask _carsLayer;

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
                Debug.Log($"water collision, water go is {collision.gameObject.name}");
            }

            if (_isOnLog && (collisionLayer & _wallLayer) > 0)
            {
                Debug.Log("collision with wall!");
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