using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameCore
{
    public class RowControllerT
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

        private (float dist, int nextNumb, MovingItem item) _currentLastInfo;

        public RowControllerT(RowData rowData,
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
                var item = _currentLastInfo.item;

                if (IsNeedNext())
                {
                    var nextXPos = item.transform.position.x -
                        item.MoveDirection * (item.HalfX + _currentLastInfo.dist);

                    AddItem(nextXPos, _currentLastInfo.nextNumb);
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
                AddItem(_currentLastInfo.nextNumb);
            }

            if (_itemsQueue.Peek().item
                .IsBoardIntersectedX(-_direction, _unspawnBorderX))
            {
                PoolFirstItem();
            }
        }

        private bool IsNeedNext()
        {
            return (_currentLastInfo.item.IsBoardIntersectedX(
                        -_direction,
                        -_direction * (_cameraX - _currentLastInfo.dist)));
        }

        private int ChooseNumber()
        {
            if (_itemsRowData.Length > 1)
            {
                var checkNumb = UnityEngine.Random.Range(1, _itemsRowData.Length);

                var randomProb = UnityEngine.Random.value;

                if (randomProb < _itemsRowData[checkNumb].AppearProb)
                {
                    return checkNumb;
                }
            }

            return 0;
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

            var nextNumb = ChooseNumber();

            _currentLastInfo = (nextDistance, nextNumb, item);
        }

        private void PoolFirstItem()
        {
            var info = _itemsQueue.Dequeue();

            _itemsRowData[info.typeNumb].Pool
                .Unspawn(info.item);
        }
    }

    [Serializable]
    public struct RowData
    {
        public float ZPos;
        public float Speed;
        public float[] DistanceRange;
        public ItemRowData[] ItemsData;


        public RowData(float speed,
            float zPos,
            float[] distanceRange,
            ItemRowData[] itemsData)
        {
            Speed = speed;
            ZPos = zPos;
            DistanceRange = distanceRange;
            ItemsData = itemsData;
        }
    }

    [Serializable]
    public struct ItemRowData
    {
        public float AppearProb;
        public float[] LengthScaleRange;
        public MovingItem Prefab;

        public MovingItemPool Pool
        {
            readonly get => _pool;
            set => _pool = value;
        }

        public float GetRandomLength()
        {
            return UnityEngine.Random.Range(
                LengthScaleRange[0],
                LengthScaleRange[^1]);
        }
        

        private MovingItemPool _pool;
    }
}