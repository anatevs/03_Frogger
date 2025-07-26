using VContainer.Unity;

namespace GameManagement
{
    public class GameManager :
        IStartable
    {
        private GameListenersManager _listenersManager;

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