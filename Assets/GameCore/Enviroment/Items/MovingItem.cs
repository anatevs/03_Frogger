using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class MovingItem : MonoBehaviour
    {
        public float HalfX
        {
            get => _halfX;
            protected set { _halfX = value; }
        }

        public float Speed => _speed;

        public int MoveDirection => _moveDirectionX;

        private float _speed;

        private int _moveDirectionX = 1;

        private float _halfX;

        private Vector3 _directionVector = Vector3.right;

        public bool IsBoardIntersectedX(int boardDirection, float xPos)
        {
            var boardPos = transform.position.x + boardDirection * _halfX;

            return boardPos * _moveDirectionX > xPos * _moveDirectionX;
        }

        public virtual void Init(float speed, (float x, float z) startPos, float lengthScale)
        {
            SetLength(lengthScale);

            _speed = speed;

            _moveDirectionX = Math.Sign(speed);

            SetFirstBoardPosition(startPos);
        }

        protected virtual void Awake()
        {
            var boxCollider = GetComponent<BoxCollider>();

            _halfX = boxCollider.size.x / 2;
        }

        protected virtual void Update()
        {
            transform.Translate(_directionVector * _speed * Time.deltaTime);
        }

        protected virtual void SetLength(float lengthScale) { }

        private void SetFirstBoardPosition((float x, float z) startPos)
        {
            transform.position = new Vector3(
                startPos.x - _moveDirectionX * _halfX,
                transform.position.y, startPos.z);
        }
    }
}