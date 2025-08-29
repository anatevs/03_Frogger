using UnityEngine;

namespace GameCore
{
    public class Turtles : MovingItem
    {
        [SerializeField]
        private Transform[] _viewTransforms;

        private readonly float _lookRot = 180;

        protected override void SetLookDirection()
        {
            var rotPoint = transform.position + Vector3.right * _boxCollider.center.x;

            for (int i = 0; i < _viewTransforms.Length; i++)
            {
                _viewTransforms[i].RotateAround(rotPoint, Vector3.up, _lookRot);
            }

            _lookDirection = _moveDirectionX;
        }
    }
}