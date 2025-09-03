using GameCore;
using System;
using VContainer.Unity;

namespace UI
{
    public class LifesPanelController :
        IInitializable,
        IDisposable
    {
        private readonly LifesPanel _lifesView;

        private readonly PlayerLifes _playerLifes;

        public LifesPanelController(LifesPanel panelView,
            PlayerLifes playerLifes)
        {
            _lifesView = panelView;
            _playerLifes = playerLifes;
        }

        void IInitializable.Initialize()
        {
            _playerLifes.OnLifesSet += _lifesView.ShowLifes;

            _playerLifes.OnLifeDecrease += _lifesView.HideLife;
        }

        void IDisposable.Dispose()
        {
            _playerLifes.OnLifesSet -= _lifesView.ShowLifes;

            _playerLifes.OnLifeDecrease -= _lifesView.HideLife;
        }
    }
}