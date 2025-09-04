using System;

namespace GameCore
{
    public sealed class PlayerLifes
    {
        public event Action<int> OnLifesSet;

        public event Action<int> OnLifeDecrease;

        public int Lifes => _lifes;

        private int _lifes;

        public void SetLifes(int lifes)
        {
            _lifes = lifes;

            OnLifesSet?.Invoke(_lifes);
        }

        public bool TryTakeOneLife()
        {
            DecreaseLifes(1);

            return (_lifes > 0);
        }

        private void DecreaseLifes(int amount)
        {
            var currentIndex = _lifes;

            _lifes -= amount;

            for (int i = currentIndex - 1; i >= _lifes; i--)
            {
                OnLifeDecrease?.Invoke(i);
            }
        }
    }
}