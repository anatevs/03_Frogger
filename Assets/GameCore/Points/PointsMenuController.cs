using UI;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class PointsMenuController : MonoBehaviour
    {
        [SerializeField]
        private PointsView _view;

        [SerializeField]
        private PointsView _viewLevel;

        [SerializeField]
        private float _animDuration;

        private PointsTextController[] _controllers;

        [Inject]
        public void Construct(PointsStorageManager storages)
        {
            _view.SetupDuration(_animDuration);
            _viewLevel.SetupDuration(_animDuration);

            _controllers = new PointsTextController[2];

            _controllers[0] = new PointsTextController(_view, storages.TotalStorage);
            _controllers[1] = new PointsTextController(_viewLevel, storages.LevelStorage);
        }

        private void OnEnable()
        {
            foreach (var controller in _controllers)
            {
                controller.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (var controller in _controllers)
            {
                controller.Disable();
            }
        }
    }
}