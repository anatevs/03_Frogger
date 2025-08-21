using GameManagement;

namespace UI
{
    public class GameplayUIController :
        IStartGameListener,
        IResumeListener
    {
        private GameplayUIView _view;

        private GameListenersManager _listenersManager;

        public GameplayUIController(GameplayUIView view,
            GameListenersManager listenersManager)
        {
            _view = view;
            _listenersManager = listenersManager;
        }

        public void OnStartGame()
        {
            Show();
        }

        public void OnResume()
        {
            Show();
        }

        public void Show()
        {
            _view.OnPauseClicked += Hide;

            _view.Show();
        }

        public void Hide()
        {
            _listenersManager.PauseGame();

            _view.OnPauseClicked -= Hide;

            _view.Hide();
        }
    }
}