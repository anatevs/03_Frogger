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

        private readonly Dictionary<string, MovingItemPool> _pools = new();

        public MovingItemPool GetPool(string name)
        {
            return _pools[name];
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

                _pools.Add(prefab.Id, pool);
            }
        }
    }
}