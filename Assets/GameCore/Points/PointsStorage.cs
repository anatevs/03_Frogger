using System;

namespace GameCore
{
    public class PointsStorage
    {
        public event Action<int, int, int> OnPointsChanged;

        public int Value => _points;

        public int LevelValue => _levelPoints;

        private int _points = 0;

        private int _roundPoints = 0;

        private int _levelPoints = 0;

        public void Init(int value)
        {
            _points = value;
        }

        public void ChangeValue(int amount)
        {
            OnPointsChanged?.Invoke(_points, _levelPoints, amount);

            _points += amount;

            _levelPoints += amount;

            _roundPoints += amount;
        }

        public void OnDamage()
        {
            OnPointsChanged?.Invoke(_points, _levelPoints, -_roundPoints);

            _points -= _roundPoints;

            _levelPoints -= _roundPoints;

            _roundPoints = 0;
        }

        public void OnEndRound()
        {
            _roundPoints = 0;
        }

        public void OnEndLevel()
        {
            _levelPoints = 0;
        }
    }
}