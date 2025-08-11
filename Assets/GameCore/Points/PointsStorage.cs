using System;
using System.Collections;
namespace GameCore
{
    public class PointsStorage
    {
        public event Action<int, int> OnPointsChanged;

        public int Value => _points;

        private int _points = 0;

        public void Init(int value)
        {
            _points = value;
        }

        public void ChangeValue(int amount)
        {
            OnPointsChanged?.Invoke(_points, amount);

            _points += amount;
        }
    }
}