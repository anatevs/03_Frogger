using GameCore;
using System;
using System.Collections;
using UnityEngine;
using VContainer.Unity;

namespace UI
{
    public class PauseMenuController
    {
        public event Action OnResetClicked;

        private readonly PauseMenuView _view;

        private readonly PointsStorageManager _storages;

        public PauseMenuController(PauseMenuView view,
            PointsStorageManager storages)
        {
            _view = view;

            _storages = storages;
        }

        public void Show()
        {
            var bestText = _storages.BestScore.ToString();

            _view.Show(bestText);

            _view.OnResumeClicked += Hide;
        }

        public void Hide()
        {
            _view.OnResumeClicked -= Hide;

            _view.Hide();
        }
    }
}