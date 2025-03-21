using DG.Tweening;
using GameCore;
using UnityEngine;

namespace Assets.GameCore.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private FrogAnimation _frogAnimation;

        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _jumpDuration;

        private bool _isMoving;

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
            if (!_isMoving)
            {
                _isMoving = true;

                transform.rotation = Quaternion.LookRotation(direction);

                transform.DOMove(transform.position + direction, _jumpDuration)
                    .OnComplete(EndMove);

                _frogAnimation.Jump();
            }
        }

        private void EndMove()
        {
            _isMoving = false;
        }
    }
}