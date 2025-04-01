using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class LogController : MonoBehaviour
    {
        public float HalfX => _halfX;

        private float _speed;

        private int _moveDirectionX = Direction.Right;

        private Vector3 _directionVector = Vector3.right;

        private float _halfX;

        private float _defaultHalfX;

        public bool IsBoardIntersectedX(int boardDirection, float xPos)
        {
            var boardPos = transform.position.x + boardDirection * _halfX;

            return boardPos * _moveDirectionX > xPos * _moveDirectionX;
        }

        public float GetLength()
        {
            var bxCollider = GetComponent<BoxCollider>();

            return bxCollider.size.x;
        }

        private void Update()
        {
            transform.Translate(_directionVector * _speed * Time.deltaTime);
        }

        public void Init(float speed, float length, float defaultLenght, Transform parent, Vector3 boardPos)
        {
            _speed = speed;
            _moveDirectionX = Math.Sign(speed);

            _defaultHalfX = defaultLenght / 2;
            SetLength(length);

            transform.SetParent(parent);
            SetFirstBoardPosition(boardPos);
        }

        private void SetLength(float length)
        {
            transform.localScale = new Vector3(length / transform.localScale.x, 1, 1);

            _halfX = _defaultHalfX * length;
        }

        private void SetFirstBoardPosition(Vector3 boardPos)
        {
            transform.localPosition = new Vector3(
                boardPos.x - _moveDirectionX * _halfX,
                boardPos.y, boardPos.z);
        }

    }
}