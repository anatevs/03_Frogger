using DG.Tweening;
using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Player : MonoBehaviour
    {
        public event Action<Transform> OnWin;

        public event Action OnDropped;

        public event Action OnCarHit;

        [SerializeField]
        private FrogAnimation _frogAnimation;

        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _jumpDuration;

        [SerializeField]
        private Transform _defaultParent;

        [SerializeField]
        private LayerMask _logsLayer;

        [SerializeField]
        private LayerMask _carsLayer;

        [SerializeField]
        private LayerMask _wallLayer;

        [SerializeField]
        private Transform _body;

        private Rigidbody _rigidbody;

        private BoxCollider _collider;

        private Vector3 _colliderCenter;

        private LayerMask _waterLayer = 1 << 4;

        private bool _isJumping;

        private Vector3 _bodyShift;

        private float _moveJumpDuration;

        private readonly float _jumpRaycastLength = 1f;

        private Vector3[] _colliderBordersX = new Vector3[2];

        private void Awake()
        {
            _frogAnimation.SetupJumpSpeed(_jumpDuration);

            _moveJumpDuration = _jumpDuration - _frogAnimation.StartJumpDelay;

            _rigidbody = GetComponent<Rigidbody>();

            _collider = GetComponent<BoxCollider>();

            _bodyShift = _collider.center - _body.transform.position;

            _colliderCenter = _collider.center;

            _colliderBordersX[0] = new Vector3(-_collider.size.x / 2, 0, 0);
            _colliderBordersX[1] = new Vector3(_collider.size.x / 2, 0, 0);

            Physics.gravity *= 10;
        }

        private void OnEnable()
        {
            _inputHandler.OnMoved += MovePlayer;
        }

        private void OnDisable()
        {
            _inputHandler.OnMoved -= MovePlayer;
        }

        private void Update()
        {
            _rigidbody.useGravity = !_isJumping;

            if (_isJumping)
            {
                var yPos = _body.position.y + _bodyShift.y;

                _collider.center = new Vector3(_collider.center.x,
                    yPos, _collider.center.z);
            }
        }

        private void MovePlayer(Vector3Int direction)
        {
            if (_isJumping)
            {
                return;
            }

            transform.rotation = Quaternion.LookRotation(direction);

            if (Physics.Raycast(transform.position + _colliderBordersX[0], direction, _jumpRaycastLength, _wallLayer) ||
                Physics.Raycast(transform.position + _colliderBordersX[1], direction, _jumpRaycastLength, _wallLayer))
            {
                Debug.Log("the wall is farther");

                return;
            }

            if (!_isJumping)
            {
                _isJumping = true;

                if (direction.x != 0)
                {
                    transform.DOMoveX(transform.position.x + direction.x, _moveJumpDuration)
                        .SetDelay(_frogAnimation.StartJumpDelay)
                        .OnComplete(EndJump);
                }
                else
                {
                    transform.DOMoveZ(transform.position.z + direction.z, _moveJumpDuration)
                        .SetDelay(_frogAnimation.StartJumpDelay)
                        .OnComplete(EndJump);
                }

                _frogAnimation.Jump();
            }
        }

        private void EndJump()
        {
            _collider.center = _colliderCenter;

            _isJumping = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _logsLayer.value) > 0)
            {
                transform.SetParent(collision.transform);
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
                transform.SetParent(_defaultParent);
            }
        }
    }
}