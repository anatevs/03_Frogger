using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class LogsManager : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float[] _lengthScaleRange = { 0.5f, 1.5f };

        [SerializeField]
        private float[] _distanceRange = { 1f, 2f };

        [SerializeField]
        private float[] _zPositions = { -2, -1, 0, 1, 2 };

        [SerializeField]
        private LogPool _pool;

        private int _moveDirection;

        private float _cameraX = 13;

        private float _startX;

        private float _endX;

        private readonly List<(float dist, LogController log)> _checkingNext = new();

        private readonly List<int> _hasNextIndexes = new();

        private void Start()
        {
            _moveDirection = Math.Sign(_speed);

            _startX = -_moveDirection * _cameraX;
            _endX = _moveDirection * _cameraX;

            InitLogs();
        }

        private void Update()
        {
            _hasNextIndexes.Clear();

            for (int i = 0; i < _checkingNext.Count; i++)
            {
                if (_checkingNext[i].log.IsBoardIntersectedX(
                    -_moveDirection,
                    _startX + _moveDirection * _checkingNext[i].dist))
                {
                    Debug.Log("next gen");

                    _hasNextIndexes.Add(i);
                }
            }

            foreach (var idx in _hasNextIndexes)
            {
                var zPos = _checkingNext[idx].log.transform.localPosition.z;

                MakeNewLog(_startX, zPos, true, out _);

                _checkingNext.RemoveAt(idx);
            }
        }

        private void InitLogs()
        {
            foreach (var zPos in _zPositions)
            {
                MakeNewLog(_endX, zPos, false, out var logInfo);

                var allInFOV = false;

                while (!allInFOV)
                {
                    if (logInfo.log.IsBoardIntersectedX(
                        -_moveDirection,
                        _startX + _moveDirection * logInfo.dist))
                    {
                        MakeNewLog(
                            logInfo.log.transform.position.x - _moveDirection * (logInfo.log.HalfX + logInfo.dist),
                            zPos, false, out logInfo);
                    }
                    else
                    {
                        _checkingNext.Add(logInfo);

                        allInFOV = true;
                    }
                }
            }
        }

        private void MakeNewLog(float xPos, float zPos, bool addToChecking, out (float dist, LogController log) result)
        {
            var nextDistance = UnityEngine.Random.Range(_distanceRange[0], _distanceRange[1]);

            var pos = new Vector3(xPos, 0, zPos);

            var log = _pool.Spawn(transform, pos, _speed, GenerateLength());

            result = (nextDistance, log);

            if (addToChecking)
            {
                _checkingNext.Add(result);
            }
        }

        private float GenerateLength()
        {
            return UnityEngine.Random.Range(_lengthScaleRange[0], _lengthScaleRange[1]);
        }
    }
}