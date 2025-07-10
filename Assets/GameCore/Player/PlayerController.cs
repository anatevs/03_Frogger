using System;
using VContainer.Unity;

namespace GameCore
{
    public class PlayerController :
        IInitializable,
        IDisposable
    {
        private readonly InputHandler _inputHandler;

        private readonly PlayerJump _playerJump;

        public PlayerController(InputHandler inputHandler, PlayerJump playerJump)
        {
            _inputHandler = inputHandler;
            _playerJump = playerJump;
        }

        void IInitializable.Initialize()
        {
            _inputHandler.OnMoved += _playerJump.MakeJump;
        }

        void IDisposable.Dispose()
        {
            _inputHandler.OnMoved -= _playerJump.MakeJump;
        }
    }
}