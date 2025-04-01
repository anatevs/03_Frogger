using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class LogPool : MonoBehaviour
    {
        [SerializeField]
        private LogController _prefab;

        [SerializeField]
        private Transform _poolTransform;

        private float _defaultLength;

        private readonly Queue<LogController> _logsQueue = new();

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _defaultLength = _prefab.GetLength();
        }

        public LogController Spawn(Transform parent, Vector3 position, float speed, float length)
        {
            if (!_logsQueue.TryDequeue(out var log))
            {
                log = Instantiate(_prefab);
            }

            log.Init(speed, length, _defaultLength, parent, position);

            log.gameObject.SetActive(true);

            return log;
        }

        public void Unspawn(LogController log)
        {
            log.transform.SetParent(_poolTransform);
            log.gameObject.SetActive(false);

            _logsQueue.Enqueue(log);
        }
    }
}