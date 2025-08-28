using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class PoolsService :
        MonoBehaviour
    {
        [SerializeField]
        private Transform _poolsTransform;

        [SerializeField]
        private MovingItem[] _prefabs;

        private readonly Dictionary<Type, MovingItemPool> _pools = new();

        private readonly Dictionary<string, MovingItemPool> _poolNames = new();

        public MovingItemPool GetPool(Type itemType)
        {
            return _pools[itemType];
        }

        public MovingItemPool GetPool(string name)
        {
            return _poolNames[name];
        }

        private void Awake()
        {
            InitPools();
        }

        private void InitPools()
        {
            foreach (var prefab in _prefabs)
            {
                var pool = new MovingItemPool(prefab, _poolsTransform);

                _pools.Add(prefab.GetType(), pool);

                _poolNames.Add(prefab.Id, pool);
            }
        }
    }
}