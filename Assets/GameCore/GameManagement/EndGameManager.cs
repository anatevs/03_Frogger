using GameCore;
using System;
using VContainer.Unity;

namespace GameManagement
{
    public class EndGameManager :
        IInitializable,
        IDisposable
    {
        private Player _player;

        private GameListenersManager _listenersManager;

        void IInitializable.Initialize()
        {
            _player.OnDropped += _listenersManager.OnEndGame;
        }

        void IDisposable.Dispose()
        {
            _player.OnDropped -= _listenersManager.OnEndGame;
        }
    }
}