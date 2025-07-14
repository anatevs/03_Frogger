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

        private readonly GameListenersManager _listenersManager;

        private bool _isAlive;

        private int _startLifes = 3;

        public PlayerController(InputHandler inputHandler,
            PlayerJump playerJump,
            PlayerCollisions player,
            PlayerLifes playerLifes)
        {
            _inputHandler = inputHandler;
            _playerJump = playerJump;
            _playerCollisions = player;
            _playerLifes = playerLifes;
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
            _isAlive = false;

            if (_playerLifes.TryTakeOneLife())
            {
                _playerCollisions.SetToStart();

                _isAlive = true;

                Debug.Log($"you have {_playerLifes.Lifes} lifes left");
            }

            else
            {
                Debug.Log("no more lifes, restart the level");
            }
        }
    }
}