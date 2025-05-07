using DG.Tweening;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField]
        private FrogAnimation _frogAnimation;

        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _jumpDuration;

        [SerializeField]
        private Transform _body;

        [SerializeField]
        private LayerMask _wallLayer;

        private Rigidbody _rigidbody;

        private BoxCollider _collider;

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
    }
}