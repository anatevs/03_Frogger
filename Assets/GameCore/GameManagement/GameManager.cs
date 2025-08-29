using VContainer.Unity;

namespace GameManagement
{
    public sealed class GameManager :
        IStartable
    {
        private readonly GameListenersManager _listenersManager;

        public GameManager(GameListenersManager listenersManager)
        {
            _listenersManager = listenersManager;
        }

        void IStartable.Start()
        {
            _listenersManager.StartGame();
        }
    }
}