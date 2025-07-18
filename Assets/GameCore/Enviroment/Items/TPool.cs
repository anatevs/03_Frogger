using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class TPool<T> where T : MovingItem
    {
        private readonly T _prefab;

        private readonly Transform _poolTransform;

        private readonly Queue<T> _logsQueue = new();

        public TPool(T prefab, Transform poolTransform)
        {
            _prefab = prefab;
            _poolTransform = poolTransform;
        }

        public T Spawn(float speed, (float x, float z) position, float lengthScale, Transform parent)
        {
            if (!_logsQueue.TryDequeue(out var item))
            {
                item = GameObject.Instantiate(_prefab);
            }

            item.transform.SetParent(parent);
            item.Init(speed, position, lengthScale);

            item.gameObject.SetActive(true);

            return item;
        }

        public void Unspawn(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_poolTransform);

            _logsQueue.Enqueue(item);
        }
    }
}