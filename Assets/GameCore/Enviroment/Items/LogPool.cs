using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class LogPool : MonoBehaviour
    {
        [SerializeField]
        private LogController _prefab;

        [SerializeField]
        private Transform _poolTransform;

        private readonly Queue<LogController> _logsQueue = new();

        public LogController Spawn(float speed, (float x, float z) position, float lengthScale, Transform parent, float endX)
        {
            if (!_logsQueue.TryDequeue(out var log))
            {
                log = Instantiate(_prefab);
            }

            log.transform.SetParent(parent);
            log.Init(speed, position, lengthScale, endX);

            log.gameObject.SetActive(true);

            return log;
        }

        public void Unspawn(LogController log)
        {
            log.gameObject.SetActive(false);
            log.transform.SetParent(_poolTransform);

            _logsQueue.Enqueue(log);
        }
    }
}