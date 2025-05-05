using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class LogController : MonoBehaviour
    {
        public event Action<LogController> OnEndPassed;

        public float HalfX => _halfX;

        public float Speed => _speed;

        public int MoveDirection => _moveDirectionX;

        [SerializeField]
        private Transform _view;

        private float _speed;

        private int _moveDirectionX = 1;

        private float _endX;

        private float _halfX;

        private float _defaultHalfX;

        private Vector3 _directionVector = Vector3.right;

        public bool IsBoardIntersectedX(int boardDirection, float xPos)
        {
            var boardPos = transform.position.x + boardDirection * _halfX;

            return boardPos * _moveDirectionX > xPos * _moveDirectionX;
        }

        private void Awake()
        {
            var bxCollider = GetComponent<BoxCollider>();

            _defaultHalfX = bxCollider.size.x / 2;
        }

        private void Update()
        {
            transform.Translate(_directionVector * _speed * Time.deltaTime);

            if (IsBoardIntersectedX(-_moveDirectionX, _endX))
            {
                OnEndPassed?.Invoke(this);
            }
        }

        public void Init(float speed, (float x, float z) startPos, float lengthScale, float endX)
        {
            _speed = speed;
            _moveDirectionX = Math.Sign(speed);

            SetLength(lengthScale);

            SetFirstBoardPosition(startPos);

            _endX = endX;
        }

        private void SetLength(float lengthScale)
        {
            _view.localScale = new Vector3(lengthScale, 1, 1);

            _halfX = _defaultHalfX * lengthScale;
        }

        private void SetFirstBoardPosition((float x, float z) startPos)
        {
            transform.position = new Vector3(
                startPos.x - _moveDirectionX * _halfX,
                transform.position.y, startPos.z);
        }

    }
}