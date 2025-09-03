using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameManagement;
using System;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public sealed class PlayerController :
        IInitializable,
        IDisposable,
        IPauseListener,
        IResumeListener
    {
        private readonly InputHandler _inputHandler;

        private readonly PlayerJump _playerJump;

        private readonly PlayerCollisions _playerCollisions;

        private readonly PlayerLifes _playerLifes;

        private readonly PlayerView _playerView;

        private readonly GameListenersManager _listenersManager;

        private readonly TimeCounter _timeCounter;

        private bool _isAlive = true;

        public PlayerController(InputHandler inputHandler,
            PlayerJump playerJump,
            PlayerCollisions player,
            PlayerLifes playerLifes,
            GameListenersManager listenersManager,
            TimeCounter timeCounter)
        {
            _inputHandler = inputHandler;
            _playerJump = playerJump;
            _playerCollisions = player;
            _playerLifes = playerLifes;

            if (_playerCollisions.TryGetComponent(out _playerView) == false)
            {
                Debug.LogError("PlayerView component is not found on PlayerCollisions");
            }

            _listenersManager = listenersManager;
            _timeCounter = timeCounter;
        }

        void IInitializable.Initialize()
        {
            _inputHandler.OnMoved += MakeJump;

            _playerCollisions.OnDamaged += MakeOnDamage;

            _timeCounter.OnTimeIsUp += MakeOnDamage;
        }

        void IDisposable.Dispose()
        {
            _inputHandler.OnMoved -= MakeJump;

            _playerCollisions.OnDamaged -= MakeOnDamage;

            _timeCounter.OnTimeIsUp -= MakeOnDamage;
        }

        public void OnPause()
        {
            _isAlive = false;
        }

        public void OnResume()
        {
            _isAlive = true;
        }

        private void MakeJump(Vector3Int direction)
        {
            if (_isAlive)
            {
                _playerJump.MakeJump(direction);
            }
        }

        private void MakeOnDamage()
        {
            ShowDamage().Forget();
        }

        private async UniTaskVoid ShowDamage()
        {
            _isAlive = false;
            _playerView.ShowFrog(false);

            await _playerView.DeathAnimation()
                .Play()
                .AsyncWaitForCompletion();

            if (_playerLifes.TryTakeOneLife())
            {
                _playerCollisions.SetToStart();

                _isAlive = true;
                _playerView.ShowFrog(true);

                _listenersManager.RestartRound();

                Debug.Log($"you have {_playerLifes.Lifes} lifes left");
            }

            else
            {
                Debug.Log("no more lifes, restart the level");
            }
        }
    }
}