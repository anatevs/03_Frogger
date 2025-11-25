using System;
using VContainer.Unity;

namespace GameManagement
{
    public sealed class GameManager :
        IStartable,
        IDisposable
    {
        private readonly GameListenersManager _listenersManager;

        private readonly SaveLoadManager _saveLoadManager;

        private readonly AppEndManager _appEndManager;

        public GameManager(GameListenersManager listenersManager,
            SaveLoadManager saveLoadManager,
            AppEndManager appEndManager)
        {
            _listenersManager = listenersManager;
            _saveLoadManager = saveLoadManager;
            _appEndManager = appEndManager;
        }

        void IStartable.Start()
        {
            _saveLoadManager.Load();

            _listenersManager.StartGame();

            _appEndManager.OnStopped += MakeAtAppStopped;
        }
        void IDisposable.Dispose()
        {
            _appEndManager.OnStopped -= MakeAtAppStopped;
        }

        private void MakeAtAppStopped()
        {
            _saveLoadManager.Save();
        }
    }
}