using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class RowController
    {
        private readonly float _zPos;

        private readonly float _speed;

        private readonly int _direction;

        private readonly float[] _distanceRange = { 1f, 2f };

        private readonly float[] _lengthScaleRange = { 0.5f, 1.5f };

        private readonly LogPool _pool;

        private readonly Queue<LogComponent> _logsQueue = new();

        private readonly Transform _parentTransform;

        private readonly float _cameraX;

        private readonly float _unspawnBorderX;

        private (float dist, LogComponent log) _currentLastInfo;

        public RowController(float zPos, float speed,
            float[] distanceRange, float[] lengthScaleRange,
            float cameraX,
            Transform parentTransform, LogPool pool)
        {
            _zPos = zPos;
            _speed = speed;
            _direction = Math.Sign(speed);
            _distanceRange = distanceRange;
            _lengthScaleRange = lengthScaleRange;
            _cameraX = cameraX;
            _parentTransform = parentTransform;
            _pool = pool;

            _unspawnBorderX = _direction * _cameraX;

            InitLogs();
        }

        public void UpdateRow()
        {
            if (IsNeedNext())
            {
                AddItem();
            }

            if (_logsQueue.Peek().IsBoardIntersectedX(-_direction, _unspawnBorderX))
            {
                PoolFirstItem();
            }
        }

        private void InitLogs()
        {
            AddItem(_unspawnBorderX);

            var allInFOV = false;

            while (!allInFOV)
            {
                if (IsNeedNext())
                {
                    var nextXPos = _currentLastInfo.log.transform.position.x -
                        _currentLastInfo.log.MoveDirection * (_currentLastInfo.log.HalfX + _currentLastInfo.dist);

                    AddItem(nextXPos);
                }
                else
                {
                    allInFOV = true;
                }
            }
        }

        private bool IsNeedNext()
        {
            return (_currentLastInfo.log.IsBoardIntersectedX(
                        -_direction,
                        -_direction * (_cameraX - _currentLastInfo.dist)));
        }

        private void AddItem()
        {
            AddItem(-_direction * _cameraX);
        }

        private void AddItem(float xPos)
        {
            var nextDistance = UnityEngine.Random.Range(_distanceRange[0], _distanceRange[1]);

            var lengthScale = UnityEngine.Random.Range(_lengthScaleRange[0], _lengthScaleRange[1]);

            var pos = (xPos, _zPos);

            var log = _pool.Spawn(_speed, pos, lengthScale, _parentTransform, _unspawnBorderX);

            _logsQueue.Enqueue(log);

            _currentLastInfo = (nextDistance, log);
        }

        private void PoolFirstItem()
        {
            var log = _logsQueue.Dequeue();

            _pool.Unspawn(log);
        }
    }
}