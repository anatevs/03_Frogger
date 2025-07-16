using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class LogPoolv1 : MonoBehaviour
    {
        [SerializeField]
        private LogItem _prefab;

        [SerializeField]
        private Transform _poolTransform;

        private readonly Queue<LogItem> _logsQueue = new();

        public LogItem Spawn(float speed, (float x, float z) position, float lengthScale, Transform parent, float borderX)
        {
            if (!_logsQueue.TryDequeue(out var log))
            {
                log = Instantiate(_prefab);
            }

            log.transform.SetParent(parent);
            log.Init(speed, position, lengthScale, borderX);

            log.gameObject.SetActive(true);

            return log;
        }

        public void Unspawn(LogItem log)
        {
            log.gameObject.SetActive(false);
            log.transform.SetParent(_poolTransform);

            _logsQueue.Enqueue(log);
        }
    }
}