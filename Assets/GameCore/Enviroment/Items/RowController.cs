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

        private readonly ItemRowData[] _itemsRowData;

        private readonly Queue<(int typeNumb, MovingItem item)> _itemsQueue = new();

        private readonly Transform _parentTransform;

        private readonly float _cameraX;

        private readonly float _unspawnBorderX;

        private (float dist, int nextNumb, MovingItem item) _currentLast;

        public RowController(RowData rowData,
            float cameraX, Transform parentTransform)
        {
            _zPos = rowData.ZPos;
            _speed = rowData.Speed;
            _direction = Math.Sign(_speed);
            _distanceRange = rowData.DistanceRange;

            _cameraX = cameraX;
            _parentTransform = parentTransform;

            _itemsRowData = rowData.ItemsData;


            _unspawnBorderX = _direction * _cameraX;

            InitLogs();
        }

        private void InitLogs()
        {
            AddItem(_unspawnBorderX, 0);

            var allInFOV = false;

            while (!allInFOV)
            {
                var item = _currentLast.item;

                if (IsNeedNext())
                {
                    var nextXPos = item.transform.position.x -
                        item.MoveDirection * (item.HalfX + _currentLast.dist);

                    AddItem(nextXPos, _currentLast.nextNumb);
                }
                else
                {
                    allInFOV = true;
                }
            }
        }

        public void UpdateRow()
        {
            if (IsNeedNext())
            {
                AddItem(_currentLast.nextNumb);
            }

            if (_itemsQueue.Peek().item
                .IsBoardIntersectedX(-_direction, _unspawnBorderX))
            {
                PoolFirstItem();
            }
        }

        private bool IsNeedNext()
        {
            return (_currentLast.item.IsBoardIntersectedX(
                        -_direction,
                        -_direction * (_cameraX - _currentLast.dist)));
        }

        private void AddItem(int typeNumb)
        {
            AddItem(-_direction * _cameraX, typeNumb);
        }

        private void AddItem(float xPos, int typeNumb)
        {
            var lengthScale = _itemsRowData[typeNumb].GetRandomLength();

            var pos = (xPos, _zPos);

            var item = _itemsRowData[typeNumb].Pool.Spawn(_speed, pos, lengthScale, _parentTransform);

            _itemsQueue.Enqueue((typeNumb, item));

            var nextDistance = UnityEngine.Random.Range(_distanceRange[0], _distanceRange[1]);

            var nextNumb = ChooseNumber(typeNumb);

            _currentLast = (nextDistance, nextNumb, item);
        }

        private int ChooseNumber(int currentNumb)
        {
            if (_itemsRowData.Length > 1)
            {
                var checkNumb = UnityEngine.Random.Range(1, _itemsRowData.Length);

                var randomProb = UnityEngine.Random.value;

                if (randomProb < _itemsRowData[checkNumb].AppearProb &&
                    checkNumb != currentNumb)
                {
                    return checkNumb;
                }
            }

            return 0;
        }

        private void PoolFirstItem()
        {
            var (typeNumb, item) = _itemsQueue.Dequeue();

            _itemsRowData[typeNumb].Pool
                .Unspawn(item);
        }
    }
}