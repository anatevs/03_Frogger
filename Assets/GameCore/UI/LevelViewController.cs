using System;
using UI;
using VContainer.Unity;

namespace GameCore
{
    public class LevelViewController : 
        IInitializable,
        IDisposable
    {
        private readonly LevelManager _levelManager;

        private readonly LevelView _view;

        public LevelViewController(LevelManager levelManager,
            LevelView view)
        {
            _levelManager = levelManager;
            _view = view;
        }

        void IInitializable.Initialize()
        {
            _levelManager.OnLevelChanged += SetLevelNumber;
        }

        void IDisposable.Dispose()
        {
            _levelManager.OnLevelChanged -= SetLevelNumber;
        }

        private void SetLevelNumber(int levelIndex)
        {
            _view.SetLevelNumber(levelIndex + 1);
        }
    }
}