using System;
using System.Collections;
using VContainer.Unity;

namespace GameCore
{
    public class PointsCounter :
        IInitializable,
        IDisposable
    {
        private int _zMoveCost = 50;


        private PointsStorage _storage;

        private PlayerJump _playerJump;

        public PointsCounter(PointsStorage storage,
            PlayerJump playerJump)
        {
            _storage = storage;
            _playerJump = playerJump;
        }

        void IInitializable.Initialize()
        {
            _playerJump.OnZMove += CalcFromZMove;
        }

        void IDisposable.Dispose()
        {
            _playerJump.OnZMove -= CalcFromZMove;
        }

        private void CalcFromZMove(int zDirection)
        {
            _storage.ChangeValue(zDirection * _zMoveCost);
        }
    }
}