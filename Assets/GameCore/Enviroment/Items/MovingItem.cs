using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class MovingItem : MonoBehaviour
    {
        public event Action<MovingItem> OnBorderPassed;

        public float HalfX
        {
            get => _halfX;
            protected set { _halfX = value; }
        }

        public float Speed => _speed;

        public int MoveDirection => _moveDirectionX;

        //[SerializeField]
        //private Transform _view;

        //private BoxCollider _boxCollider;

        private float _speed;

        private int _moveDirectionX = 1;

        private float _borderX;

        private float _halfX;

        //private float _defaultHalfX;

        private Vector3 _directionVector = Vector3.right;

        public bool IsBoardIntersectedX(int boardDirection, float xPos)
        {
            var boardPos = transform.position.x + boardDirection * _halfX;

            return boardPos * _moveDirectionX > xPos * _moveDirectionX;
        }

        protected virtual void Awake()
        {
            var boxCollider = GetComponent<BoxCollider>();

            _halfX = boxCollider.size.x / 2;

            //_defaultHalfX = _boxCollider.size.x / 2;
        }

        protected virtual void Update()
        {
            transform.Translate(_directionVector * _speed * Time.deltaTime);

            if (IsBoardIntersectedX(-_moveDirectionX, _borderX))
            {
                OnBorderPassed?.Invoke(this);
            }
        }

        public virtual void Init(float speed, (float x, float z) startPos, float borderX)//, float lengthScale)
        {
            //SetLength(lengthScale);

            _speed = speed;

            _moveDirectionX = Math.Sign(speed);

            SetFirstBoardPosition(startPos);

            _borderX = borderX;
        }

        //private void SetLength(float lengthScale)
        //{
        //    _view.localScale = new Vector3(lengthScale, 1, 1);

        //    _halfX = _defaultHalfX * lengthScale;

        //    _boxCollider.size = new Vector3(
        //        _halfX * 2,
        //        _boxCollider.size.y,
        //        _boxCollider.size.z
        //        );
        //}

        private void SetFirstBoardPosition((float x, float z) startPos)
        {
            transform.position = new Vector3(
                startPos.x - _moveDirectionX * _halfX,
                transform.position.y, startPos.z);
        }
    }
}