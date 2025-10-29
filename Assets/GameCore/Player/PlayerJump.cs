using DG.Tweening;
using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class PlayerJump : MonoBehaviour
    {
        public event Action OnJumpEnd;

        public event Action<int> OnFrwdMove;

        [SerializeField]
        private FrogAnimation _frogAnimation;

        [SerializeField]
        private float _jumpDuration;

        [SerializeField]
        private Transform _body;

        [SerializeField]
        private LayerMask _wallLayer;

        [SerializeField]
        private LayerMask _landingLayer;

        private BoxCollider _collider;

        private Rigidbody _rigidbody;

        private Vector3 _colliderCenter;

        private bool _isJumping;

        private Vector3 _bodyShift;

        private float _moveJumpDuration;

        private readonly float _jumpRaycastLength = 1f;

        private readonly Vector3[] _colliderBordersX = new Vector3[2];

        private void Awake()
        {
            _frogAnimation.SetupJumpSpeed(_jumpDuration);

            _moveJumpDuration = _jumpDuration - _frogAnimation.StartJumpDelay;

            _collider = GetComponent<BoxCollider>();

            _rigidbody = GetComponent<Rigidbody>();

            _bodyShift = _collider.center - _body.transform.position;

            _colliderCenter = _collider.center;

            _colliderBordersX[0] = new Vector3(-_collider.size.x / 2, 0, 0);
            _colliderBordersX[1] = new Vector3(_collider.size.x / 2, 0, 0);

            _rigidbody.useGravity = false;

            Physics.gravity *= 10;
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

        public void MakeJump(Vector3Int direction)
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
                Debug.Log($"start jump pos: {transform.position}, collider: {_collider.center}, time: {Time.time}");
                Debug.Log($"jump duration: {_moveJumpDuration}, jump delay: {_frogAnimation.StartJumpDelay}");

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

                    if (direction.z > 0)
                    {
                        OnFrwdMove?.Invoke(direction.z);
                    }
                }

                _frogAnimation.Jump();
            }
        }

        private void EndJump()
        {
            Physics.Raycast(_collider.bounds.center, Vector3.down, out var hitInfo, 3f, _landingLayer);

            var yPos = 0f;

            if (hitInfo.collider != null)
            {
                Debug.Log($"landing object: {hitInfo.collider.gameObject}, tr: {transform.position}, collider: {_collider.center}, hitPt:{hitInfo.point}, time: {Time.time}");

                yPos = hitInfo.point.y + _collider.size.y / 2;
            }

            transform.position = new Vector3(
                transform.position.x,
                yPos - _colliderCenter.y,
                transform.position.z);

            _collider.center = _colliderCenter;

            _isJumping = false;

            OnJumpEnd?.Invoke();
        }
    }
}