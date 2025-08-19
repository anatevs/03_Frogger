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
        private readonly PointsStorageController _storages;

        private readonly PlayerJump _playerJump;

        private readonly TimeCounter _timeCounter;

        private int _fowrdMoveReward = 1;

        private int _roundEndReward = 5;

        private int _levelEndReward = 100;

        private int _flyOrFriendReward = 20;

        private int _halfSecondMultiplier = 1;

        private int _extraReward = 0;

        private int _ticksReward = 0;

        public PointsCounter(PointsStorageController storages,
            PlayerJump playerJump,
            TimeCounter timeCounter)
        {
            _storages = storages;
            _playerJump = playerJump;
            _timeCounter = timeCounter;
        }

        public void AddExtraPoints()
        {
            _extraReward += _flyOrFriendReward;
        }

        public void OnDamage()
        {
            _extraReward = 0;

            _storages.OnDamage();
        }

        public void OnEndRound()
        {
            _ticksReward = _timeCounter.CurrentTick * _halfSecondMultiplier;

            _storages.ChangeValue(_roundEndReward + _extraReward + _ticksReward);

            _extraReward = 0;

            _ticksReward = 0;

            _storages.OnEndRound();

            _timeCounter.StartTimer();
        }

        public void OnEndLevel()
        {
            _storages.ChangeValue(_levelEndReward);

            _extraReward = 0;

            _ticksReward = 0;

            _storages.OnEndLevel();
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
            _storages.ChangeValue(_fowrdMoveReward);
        }
    }
}