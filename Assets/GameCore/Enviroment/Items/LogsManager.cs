using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public sealed class LogsManager : MonoBehaviour
    {
        [SerializeField]
        private float[] _lengthScaleRange = { 0.5f, 1.5f };

        [SerializeField]
        private float[] _distanceRange = { 1f, 2f };

        [SerializeField]
        private RowInfo[] _rowsInfo;

        [SerializeField]
        private LogPool _pool;

        private RowController[] _rowsControllers;

        private float[] _zPositions;

        private float _cameraX;

        [Inject]
        private void Construct(CameraBorders cameraBorders)
        {
            _cameraX = cameraBorders.CameraHalfX;
        }

        private void Start()
        {
            _zPositions = new float[_rowsInfo.Length];

            _rowsControllers = new RowController[_rowsInfo.Length];

            for (int i = 0; i < _rowsInfo.Length; i++)
            {
                _zPositions[i] = _rowsInfo[i].ZPos + transform.position.z;

                _rowsControllers[i] = new RowController(
                    _zPositions[i],
                    _rowsInfo[i].Speed,
                    _distanceRange,
                    _lengthScaleRange,
                    _cameraX,
                    transform,
                    _pool);
            }
        }

        private void Update()
        {
            foreach(var rowController in _rowsControllers)
            {
                rowController.UpdateRow();
            }
        }
    }

    [Serializable]
    public struct RowInfo
    {
        public int ZPos;

        public float Speed;

        public RowInfo(int zPos, float speed)
        {
            ZPos = zPos;
            Speed = speed;
        }
    }
}