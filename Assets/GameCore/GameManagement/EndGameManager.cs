using GameCore;
using System;
using VContainer.Unity;

namespace GameManagement
{
    public class EndGameManager :
        IInitializable,
        IDisposable
    {
        private PlayerCollisions _playerCollisions;

        private GameListenersManager _listenersManager;

        void IInitializable.Initialize()
        {
            //_player.OnDropped += _listenersManager.EndGame;
        }

        void IDisposable.Dispose()
        {
            //_player.OnDropped -= _listenersManager.EndGame;
        }
    }
}