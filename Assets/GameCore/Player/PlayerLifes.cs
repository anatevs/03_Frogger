using System;

namespace GameCore
{
    public class PlayerLifes
    {
        public event Action<int> OnLifesChanged;

        public int Lifes => _lifes;

        private int _lifes;

        public void SetLifes(int lifes)
        {
            _lifes = lifes;

            OnLifesChanged?.Invoke(_lifes);
        }

        public bool TryTakeOneLife()
        {
            if (_lifes > 0)
            {
                DecreaseLifes(1);
                return true;
            }

            return false;
        }

        private void DecreaseLifes(int amount)
        {
            _lifes -= amount;

            OnLifesChanged?.Invoke(_lifes);
        }
    }
}