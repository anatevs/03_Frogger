using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class TPoolsService :
        MonoBehaviour
    {
        [SerializeField]
        private Transform _poolsTransform;

        [SerializeField]
        private LogItem _logPrefab;

        [SerializeField]
        private CubeItem _cubePrefab;

        [SerializeField]
        private MovingItem[] _prefabs;

        private readonly Dictionary<Type, MovingItemPool> _pools = new();

        public MovingItemPool GetPool(Type itemType)
        {
            return _pools[itemType];
        }

        private void Awake()
        {
            InitPools();
        }

        private void InitPools()
        {
            var pool1 = new MovingItemPool(_logPrefab, _poolsTransform);

            _pools.Add(typeof(LogItem), pool1);

            var pool2 = new MovingItemPool(_cubePrefab, _poolsTransform);

            _pools.Add(typeof(CubeItem), pool2);
        }
    }
}