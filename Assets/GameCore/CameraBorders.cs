using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public class CameraBorders : IInitializable
    {
        public float CameraHalfX => _cameraHalfX;

        private BoxCollider[] _horizontalBorders;

        private BoxCollider[] _verticalBorders;

        private float _cameraHalfX;

        private Camera _camera;

        public CameraBorders(BoxCollider[][] borders)
        {
            _horizontalBorders = borders[0];
            _verticalBorders = borders[1];

            _camera = Camera.main;

            _cameraHalfX = _camera.orthographicSize * _camera.aspect;
        }

        void IInitializable.Initialize()
        {
            SetupBorders();
        }

        private void SetupBorders()
        {
            var posY = _horizontalBorders[0].transform.position.y;


            var horBoxSize = _horizontalBorders[0].size.x;

            var horBoxPosX = (int)(_cameraHalfX / horBoxSize) + horBoxSize / 2;

            _horizontalBorders[0].transform.position = new Vector3(-horBoxPosX, posY, 0);
            _horizontalBorders[1].transform.position = new Vector3(horBoxPosX, posY, 0);


            var verBoxSize = _verticalBorders[0].size.z;

            var verBoxPosZ = (int)(_camera.orthographicSize / verBoxSize) + verBoxSize / 2;

            _verticalBorders[0].transform.position = new Vector3(0, posY, -verBoxPosZ);
            _verticalBorders[1].transform.position = new Vector3(0, posY, verBoxPosZ);
        }
    }
}