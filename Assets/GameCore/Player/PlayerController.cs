using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameManagement;
using System;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public class PlayerController :
        IInitializable,
        IDisposable
    {
        private readonly InputHandler _inputHandler;

        private readonly PlayerJump _playerJump;

        private readonly PlayerCollisions _playerCollisions;

        private readonly PlayerLifes _playerLifes;

        private readonly PlayerView _playerView;

        private readonly GameListenersManager _listenersManager;

        private bool _isAlive = true;

        private int _startLifes = 10;

        public PlayerController(InputHandler inputHandler,
            PlayerJump playerJump,
            PlayerCollisions player,
            PlayerLifes playerLifes)
        {
            _inputHandler = inputHandler;
            _playerJump = playerJump;
            _playerCollisions = player;
            _playerLifes = playerLifes;

            if (_playerCollisions.TryGetComponent(out _playerView) == false)
            {
                Debug.LogError("PlayerView component is not found on PlayerCollisions");
            }
        }

        void IInitializable.Initialize()
        {
            _playerLifes.SetLifes(_startLifes);

            _inputHandler.OnMoved += MakeJump;

            _playerCollisions.OnDamaged += MakeOnDamage;

            
        }

        void IDisposable.Dispose()
        {
            _inputHandler.OnMoved -= MakeJump;

            _playerCollisions.OnDamaged -= MakeOnDamage;
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

                Debug.Log($"you have {_playerLifes.Lifes} lifes left");
            }

            else
            {
                Debug.Log("no more lifes, restart the level");
            }
        }
    }
}