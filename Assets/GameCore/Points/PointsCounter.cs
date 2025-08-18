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

        private int _fowrdMoveReward = 1;

        private int _roundEndReward = 5;

        private int _levelEndReward = 100;

        private int _flyOrFriendReward = 20;

        //private int _halfSecondReward = 1;

        private int _extraReward = 0;

        private int _ticksReward;

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

        public void SetTicksReward(int ticks)
        {
            _ticksReward = ticks;
        }

        public void OnDamage()
        {
            _extraReward = 0;

            _storage.OnDamage();
        }

        public void OnEndRound()
        {
            _storage.ChangeValue(_roundEndReward + _extraReward);

            _extraReward = 0;

            _storage.OnEndRound();
        }

        public void OnEndLevel()
        {
            _storage.ChangeValue(_levelEndReward);

            _extraReward = 0;

            _storage.OnEndLevel();
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