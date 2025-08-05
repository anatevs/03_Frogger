using UnityEngine;

namespace GameCore
{
    public class Crocodile : MovingItem
    {
        [SerializeField]
        private Transform _viewTransform;

        private readonly float _lookRot = 180;

        protected override void SetLookDirection()
        {
            var rotPoint = transform.position + Vector3.right * _boxCollider.center.x;

            _viewTransform.RotateAround(rotPoint, Vector3.up, _lookRot);

            _lookDirection = _moveDirectionX;
        }
    }
}