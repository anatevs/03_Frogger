using System;

namespace GameCore
{
    public class PlayerLifes
    {
        public event Action<int> OnLifesChanged;

        private int _lifes;

        public void SetLifes(int lifes)
        {
            _lifes = lifes;

            OnLifesChanged?.Invoke(_lifes);
        }

        public void DecreaseLifes(int amount)
        {
            _lifes -= amount;

            OnLifesChanged?.Invoke(_lifes);
        }

        public bool HasLifes()
        {
            return _lifes > 0;
        }
    }
}