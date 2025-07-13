using GameManagement;
using System;
using UnityEngine;
using VContainer;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Player : MonoBehaviour,
        IEndRoundListener
    {
        public event Action<Transform> OnWin;



        [SerializeField]
        private PlayerJump _playerJump;





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

        private PlayerLifes _playerLifes;

        private int _decreaseLifeAmount = 1;
        private int _startLifes = 3;


        [Inject]
        public void Construct(PlayerLifes playerLifes)
        {
            _playerLifes = playerLifes;


            _playerLifes.SetLifes(_startLifes);
        }

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

                Retry();
            }

            if ((collisionLayer & _waterLayer) > 0)
            {
                Debug.Log($"water collision at {Time.time}");

                foreach (var contact in collision.contacts)
                {
                    Debug.Log($"{contact.point}");
                }

                Retry();
            }

            if (_isOnLog && (collisionLayer & _wallLayer) > 0)
            {
                Debug.Log("collision with wall!");

                Retry();
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

        private void Retry()
        {
            if (_playerLifes.HasLifes())
            {
                _playerLifes.DecreaseLifes(1);

                //SetToStart();
            }

            else
            {
                Debug.Log("no more lifes, restart the level");
            }
        }
    }
}