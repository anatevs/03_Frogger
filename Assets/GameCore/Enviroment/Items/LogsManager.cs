using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public sealed class LogsManager : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float[] _lengthScaleRange = { 0.5f, 1.5f };

        [SerializeField]
        private float[] _distanceRange = { 1f, 2f };

        [SerializeField]
        private RowInfo[] _rowsInfo;

        [SerializeField]
        private LogPool _pool;

        private Transform _parentTransform;

        private float[] _zPositions;

        private float _cameraX;

        private readonly List<(float dist, LogComponent log)> _checkingNext = new();

        private readonly List<int> _hasNextIndexes = new();

        [Inject]
        private void Construct(CameraBorders cameraBorders)
        {
            _cameraX = cameraBorders.CameraHalfX;
        }

        private void Start()
        {
            _parentTransform = transform;

            _zPositions = new float[_rowsInfo.Length];

            for (int i = 0; i < _rowsInfo.Length; i++)
            {
                _zPositions[i] = _rowsInfo[i].ZPos + transform.position.z;
            }

            InitLogs();
        }

        private void Update()
        {
            _hasNextIndexes.Clear();

            for (int i = 0; i < _checkingNext.Count; i++)
            {
                if (IsNeedNext(_checkingNext[i]))
                {
                    _hasNextIndexes.Add(i);
                }
            }

            foreach (var idx in _hasNextIndexes)
            {
                MakeNewLog(_checkingNext[idx].log);

                _checkingNext.RemoveAt(idx);
            }
        }

        private void OnDisable()
        {
            var activeLogs = _parentTransform.GetComponentsInChildren<LogComponent>();
            foreach (var log in activeLogs)
            {
                log.OnEndPassed -= PoolLog;
            }
        }

        private void InitLogs()
        {
            for (int i = 0; i < _zPositions.Length; i++)
            {
                var speed = _rowsInfo[i].Speed;

                MakeNewLog(speed, (Math.Sign(speed) * _cameraX, _zPositions[i]), false, out var logInfo);

                var allInFOV = false;

                while (!allInFOV)
                {
                    if (IsNeedNext(logInfo))
                    {
                        MakeNewLog(
                            speed,
                            (logInfo.log.transform.position.x - 
                            logInfo.log.MoveDirection * (logInfo.log.HalfX + logInfo.dist),
                            _zPositions[i]),
                            false, out logInfo);
                    }
                    else
                    {
                        _checkingNext.Add(logInfo);

                        allInFOV = true;
                    }
                }
            }
        }

        private bool IsNeedNext((float dist, LogComponent log) logInfo)
        {
            var moveDirection = logInfo.log.MoveDirection;

            return (logInfo.log.IsBoardIntersectedX(
                        -moveDirection,
                        -moveDirection * (_cameraX - logInfo.dist)));
        }

        private void MakeNewLog(LogComponent prevLog)
        {
            var zPos = prevLog.transform.position.z;
            var speed = prevLog.Speed;
            var direction = prevLog.MoveDirection;

            MakeNewLog(speed, (-direction * _cameraX, zPos), true, out _);
        }

        private void MakeNewLog(float speed, (float x, float z) pos, bool addToChecking, out (float dist, LogComponent log) result)
        {
            var nextDistance = UnityEngine.Random.Range(_distanceRange[0], _distanceRange[1]);

            var lengthScale = UnityEngine.Random.Range(_lengthScaleRange[0], _lengthScaleRange[1]);

            var log = _pool.Spawn(speed, pos, lengthScale, _parentTransform, Math.Sign(speed) * _cameraX);

            result = (nextDistance, log);

            if (addToChecking)
            {
                _checkingNext.Add(result);
            }

            log.OnEndPassed += PoolLog;
        }

        private void PoolLog(LogComponent log)
        {
            log.OnEndPassed -= PoolLog;

            _pool.Unspawn(log);
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