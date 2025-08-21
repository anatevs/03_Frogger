using GameCore;
using GameManagement;

namespace UI
{
    public class PauseMenuController :
        IPauseListener
    {
        private readonly PauseMenuView _view;

        private readonly PointsStorageManager _storages;

        private readonly GameListenersManager _listenersManager;

        public PauseMenuController(PauseMenuView view,
            PointsStorageManager storages,
            GameListenersManager listenersManager)
        {
            _view = view;

            _storages = storages;

            _listenersManager = listenersManager;
        }

        public void OnPause()
        {
            Show();
        }

        public void Show()
        {
            var bestText = _storages.BestScore.ToString();

            _view.Show(bestText);

            _view.OnResumeClicked += Hide;
        }

        public void Hide()
        {
            _listenersManager.ResumeGame();

            _view.OnResumeClicked -= Hide;

            _view.Hide();
        }
    }
}