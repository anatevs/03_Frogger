using System;

namespace GameCore
{
    public class PointsStorage
    {
        public event Action<int, int> OnChanged;

        public int Value => _points;

        private int _points = 0;

        public void ChangeValue(int amount)
        {
            OnChanged?.Invoke(_points, amount);

            _points += amount;
        }
    }
}