using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class LogsManagerT : MonoBehaviour
    {
        [SerializeField]
        private RowData[] _rowData;

        [SerializeField]
        private Transform _poolTransform;

        private RowControllerT[] _rowsControllers;

        private float[] _zPositions;

        private float _cameraX;

        [Inject]
        private void Construct(CameraBorders cameraBorders)
        {
            _cameraX = cameraBorders.CameraHalfX;
        }

        private void Start()
        {
            _zPositions = new float[_rowData.Length];

            _rowsControllers = new RowControllerT[_rowData.Length];

            for (int i = 0; i < _rowData.Length; i++)
            {
                _zPositions[i] = _rowData[i].ZPos + transform.position.z;

                foreach (var itemData in _rowData[i].ItemsData)
                {
                    var type = itemData.Prefab.GetType().ToString();
                    //var pool = new TPool<type> (itemData.Prefab, _poolTransform);
                }

                //var pool = new TPool<>

                //_rowsControllers[i] = new RowController(
                //    _zPositions[i],
                //    _rowsInfo[i].Speed,
                //    _distanceRange,
                //    _lengthScaleRange,
                //    _cameraX,
                //    transform,
                //    _pool);
            }
        }

        //private void Update()
        //{
        //    foreach (var rowController in _rowsControllers)
        //    {
        //        rowController.UpdateRow();
        //    }
        //}
    }
}