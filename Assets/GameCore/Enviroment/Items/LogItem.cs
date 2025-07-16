using UnityEngine;

namespace GameCore
{
    public class LogItem : MovingItem
    {
        [SerializeField]
        private Transform _view;

        private BoxCollider _boxCollider;

        private float _defaultHalfX;

        protected override void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();

            _defaultHalfX = _boxCollider.size.x / 2;
        }

        public void Init(float speed, (float x, float z) startPos, float endX, float lengthScale)
        {
            SetLength(lengthScale);

            Init(speed, startPos, endX);
        }

        private void SetLength(float lengthScale)
        {
            _view.localScale = new Vector3(lengthScale, 1, 1);

            HalfX = _defaultHalfX * lengthScale;

            _boxCollider.size = new Vector3(
                HalfX * 2,
                _boxCollider.size.y,
                _boxCollider.size.z
                );
        }
    }
}