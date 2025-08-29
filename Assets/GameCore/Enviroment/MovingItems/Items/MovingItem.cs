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

        public int MoveDirection => _moveDirectionX;

        public string Id => _id;

        [SerializeField]
        private string _id;

        protected float _speed;

        protected int _moveDirectionX = 1;

        protected BoxCollider _boxCollider;

        protected float _halfX;

        protected int _lookDirection = 1;

        protected Vector3 _directionVector = Vector3.right;

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

            if (_lookDirection != _moveDirectionX)
            {
                SetLookDirection();
            }

            SetFirstBoardPosition(startPos);
        }

        protected virtual void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();

            _halfX = _boxCollider.size.x / 2;
        }

        protected virtual void Update()
        {
            transform.Translate(_directionVector * _speed * Time.deltaTime);
        }

        protected virtual void SetLength(float lengthScale) { }

        protected virtual void SetLookDirection() { }

        private void SetFirstBoardPosition((float x, float z) startPos)
        {
            transform.position = new Vector3(
                startPos.x - _moveDirectionX * _halfX,
                transform.position.y, startPos.z);
        }
    }
}