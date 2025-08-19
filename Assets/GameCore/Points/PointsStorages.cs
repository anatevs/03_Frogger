using System;
using UnityEngine;

namespace GameCore
{
    public class PointsStorages
    {
        public event Action<int, int> OnTotalChanged;

        public event Action<int, int> OnLevelChanged;

        public int Value => _points;

        public int LevelValue => _levelPoints;

        private int _points = 0;

        private int _roundPoints = 0;

        private int _levelPoints = 0;

        public void ChangeValue(int amount)
        {
            OnTotalChanged?.Invoke(_points, amount);

            _points += amount;


            OnLevelChanged?.Invoke(_levelPoints, amount);

            _levelPoints += amount;


            _roundPoints += amount;
        }

        public void OnDamage()
        {
            Debug.Log($"damage with {-_roundPoints}");

            ChangeValue(-_roundPoints);
        }

        public void OnEndRound()
        {
            _roundPoints = 0;
        }

        public void OnEndLevel()
        {
            OnLevelChanged?.Invoke(_levelPoints, -_levelPoints);

            _levelPoints = 0;


            _roundPoints = 0;
        }
    }
}