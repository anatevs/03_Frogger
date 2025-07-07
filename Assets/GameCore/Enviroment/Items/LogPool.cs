using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class LogPool : MonoBehaviour
    {
        [SerializeField]
        private LogComponent _prefab;

        [SerializeField]
        private Transform _poolTransform;

        private readonly Queue<LogComponent> _logsQueue = new();

        public LogComponent Spawn(float speed, (float x, float z) position, float lengthScale, Transform parent, float endX)
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

        public void Unspawn(LogComponent log)
        {
            log.gameObject.SetActive(false);
            log.transform.SetParent(_poolTransform);

            _logsQueue.Enqueue(log);
        }
    }
}