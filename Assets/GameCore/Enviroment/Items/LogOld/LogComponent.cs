using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class LogComponent : MonoBehaviour
    {
        public float HalfX => _halfX;

        public float Speed => _speed;

        public int MoveDirection => _moveDirectionX;

        [SerializeField]
        private Transform _view;

        private BoxCollider _boxCollider;

        private float _speed;

        private int _moveDirectionX = 1;

        private float _halfX;

        private float _defaultHalfX;

        private Vector3 _directionVector = Vector3.right;

        public bool IsBoardIntersectedX(int logBoardDirection, float xPos)
        {
            var boardPos = transform.position.x + logBoardDirection * _halfX;

            return boardPos * _moveDirectionX > xPos * _moveDirectionX;
        }

        public void Init(float speed, (float x, float z) startPos, float lengthScale)
        {
            _speed = speed;

            _moveDirectionX = Math.Sign(speed);

            SetLength(lengthScale);

            SetFirstBoardPosition(startPos);
        }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();

            _defaultHalfX = _boxCollider.size.x / 2;
        }

        private void Update()
        {
            transform.Translate(_directionVector * _speed * Time.deltaTime);
        }

        private void SetLength(float lengthScale)
        {
            _view.localScale = new Vector3(lengthScale, 1, 1);

            _halfX = _defaultHalfX * lengthScale;

            _boxCollider.size = new Vector3(
                _halfX * 2,
                _boxCollider.size.y,
                _boxCollider.size.z
                );
        }

        private void SetFirstBoardPosition((float x, float z) startPos)
        {
            transform.position = new Vector3(
                startPos.x - _moveDirectionX * _halfX,
                transform.position.y, startPos.z);
        }

    }
}