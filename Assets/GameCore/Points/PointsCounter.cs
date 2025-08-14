using GameManagement;
using System;
using VContainer.Unity;

namespace GameCore
{
    public class PointsCounter :
        IInitializable,
        IDisposable,
        IDamageListener,
        IRoundEndListener,
        ILevelEndListener
    {
        private readonly PointsStorage _storage;

        private readonly PlayerJump _playerJump;

        private int _fowrdMoveReward = 10;

        private int _roundEndReward = 50;

        private int _levelEndReward = 1000;

        private int _flyOrFriendReward = 200;

        //private int _halfSecondReward = 10;

        private int _extraReward = 0;

        public PointsCounter(PointsStorage storage,
            PlayerJump playerJump)
        {
            _storage = storage;
            _playerJump = playerJump;
        }

        public void AddExtraPoints()
        {
            _extraReward += _flyOrFriendReward;
        }

        public void OnDamage()
        {
            _extraReward = 0;
        }

        public void OnEndRound()
        {
            _storage.ChangeValue(_roundEndReward + _extraReward);

            _extraReward = 0;
        }

        public void OnEndLevel()
        {
            _storage.ChangeValue(_levelEndReward);

            _extraReward = 0;
        }

        void IInitializable.Initialize()
        {
            _playerJump.OnFrwdMove += CalcFromZMove;
        }

        void IDisposable.Dispose()
        {
            _playerJump.OnFrwdMove -= CalcFromZMove;
        }

        private void CalcFromZMove(int zDirection)
        {
            _storage.ChangeValue(_fowrdMoveReward);
        }
    }
}