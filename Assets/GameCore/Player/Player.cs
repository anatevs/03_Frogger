using DG.Tweening;
using UnityEngine;

namespace GameCore
{
    public class Player : MonoBehaviour
    {
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

        private LayerMask _waterLayer = 4;

        private bool _isJumping;

        private void Awake()
        {
            _frogAnimation.SetupJumpSpeed(_jumpDuration);
        }

        private void OnEnable()
        {
            _inputHandler.OnMoved += MovePlayer;
        }

        private void OnDisable()
        {
            _inputHandler.OnMoved -= MovePlayer;
        }

        private void MovePlayer(Vector3 direction)
        {
            if (!_isJumping)
            {
                _isJumping = true;

                transform.rotation = Quaternion.LookRotation(direction);

                transform.DOMove(transform.position + direction, _jumpDuration)
                    .OnComplete(EndMove);

                _frogAnimation.Jump();
            }
        }

        private void EndMove()
        {
            _isJumping = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            Debug.Log($"collision with {collision.gameObject.name} {collisionLayer}");

            if ((collisionLayer & _logsLayer.value) > 0)
            {
                //transform.SetParent(collision.transform);

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
                //transform.SetParent(_defaultParent);
            }
        }
    }
}