using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class PointsStorage : MonoBehaviour
    {
        public event Action<int> OnPointsChanged;

        public int Value => _points;

        private int _points = 100;




        [SerializeField]
        bool _isChange;

        [SerializeField]
        int _changeAmount = 10;

        private void Update()
        {
            if (_isChange)
            {
                _isChange = false;
                _points += _changeAmount;
                OnPointsChanged?.Invoke(_changeAmount);
            }
        }
    }
}