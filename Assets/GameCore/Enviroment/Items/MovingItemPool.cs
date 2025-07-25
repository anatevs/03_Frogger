﻿using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class MovingItemPool
    {
        private readonly MovingItem _prefab;

        private readonly Transform _poolTransform;

        private readonly Queue<MovingItem> _itemsQueue = new();

        public MovingItemPool(MovingItem prefab, Transform poolTransform)
        {
            _prefab = prefab;
            _poolTransform = poolTransform;
        }

        public MovingItem Spawn(float speed, (float x, float z) position, float lengthScale, Transform parent)
        {
            if (!_itemsQueue.TryDequeue(out var item))
            {
                item = GameObject.Instantiate(_prefab);
            }

            item.transform.SetParent(parent);
            item.Init(speed, position, lengthScale);

            item.gameObject.SetActive(true);

            return item;
        }

        public void Unspawn(MovingItem item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_poolTransform);

            _itemsQueue.Enqueue(item);
        }
    }
}