using System.Collections.Generic;

namespace GameCore
{
    public class ActiveLogsService
    {
        public List<MovingItem> Logs => _logs;

        private readonly MovingItemPool _logsPool;

        private readonly List<MovingItem> _logs = new();

        public ActiveLogsService(PoolsService poolsService, LogItem logPrefab)
        {
            _logsPool = poolsService.GetPool(logPrefab.Id);

            _logsPool.OnSpawn += AddItem;
            _logsPool.OnUnspawn += RemoveItem;
        }

        public void Dispose()
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