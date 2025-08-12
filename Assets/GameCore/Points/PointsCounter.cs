using GameManagement;
using System;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public class PointsCounter :
        IInitializable,
        IDisposable,
        IRoundEndListener,
        ILevelEndListener
    {
        private int _fowrdMoveReward = 10;

        private int _roundEndReward = 50;

        private int _levelEndReward = 1000;

        private int _flyOrFriendReward = 200;

        //private int _halfSecondReward = 10;

        private PointsStorage _storage;

        private PlayerJump _playerJump;

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
            Debug.Log(_extraReward);
        }

        public void OnEndRound()
        {
            _storage.ChangeValue(_roundEndReward + _extraReward);

            _extraReward = 0;
        }

        public void OnEndLevel()
        {
            _storage.ChangeValue(_levelEndReward);
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