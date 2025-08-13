using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class ActiveLogsService : MonoBehaviour
    {
        public List<MovingItem> Logs => _logs;

        [SerializeField]
        private PoolsService _poolsService;

        private MovingItemPool _logsPool;

        private readonly List<MovingItem> _logs = new();

        public void Init()
        {
            _logsPool = _poolsService.GetPool(typeof(LogItem));

            _logsPool.OnSpawn += AddItem;
            _logsPool.OnUnspawn += RemoveItem;
        }

        private void OnEnable()
        {
            if (_logsPool != null)
            {
                _logsPool.OnSpawn += AddItem;
                _logsPool.OnUnspawn += RemoveItem;
            }
        }

        private void OnDisable()
        {
            _logsPool.OnSpawn -= AddItem;
            _logsPool.OnUnspawn -= RemoveItem;
        }

        private void AddItem(MovingItem item)
        {
            _logs.Add(item);
        }

        private void RemoveItem(MovingItem item)
        {
            _logs.Remove(item);
        }
    }
}