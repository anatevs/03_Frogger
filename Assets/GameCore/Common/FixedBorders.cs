using UnityEngine;

namespace GameCore
{
    public class FixedBorders :
        IBorders
    {
        public float BordersHalfX => _bordersHalfX;

        private readonly BoxCollider[] _horizontalBorders;

        private readonly float _bordersHalfX;

        public FixedBorders(BoxCollider[] horizontalBorders)
        {
            _horizontalBorders = horizontalBorders;

            var horBoxHalfSize = _horizontalBorders[0].size.x / 2;

            _bordersHalfX = Mathf.Abs(_horizontalBorders[0].transform.position.x) - horBoxHalfSize;
        }
    }
}