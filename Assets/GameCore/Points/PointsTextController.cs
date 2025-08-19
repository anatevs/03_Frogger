using UI;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class PointsTextController : MonoBehaviour
    {
        [SerializeField]
        private PointsView _view;

        [SerializeField]
        private PointsView _viewLevel;

        private PointsStorages _storage;

        [Inject]
        public void Construct(PointsStorages storage)
        {
            _storage = storage;
        }

        private void OnEnable()
        {
            _view.SetText(_storage.Value.ToString());

            _storage.OnTotalChanged += SetupTotalText;

            _storage.OnLevelChanged += SetupLevelText;
        }

        private void OnDisable()
        {
            _storage.OnTotalChanged -= SetupTotalText;

            _storage.OnLevelChanged -= SetupLevelText;
        }

        public void SetupTotalText(int fromPoints, int deltaPoints)
        {
            _view.SetText(fromPoints, deltaPoints).Forget();
        }

        public void SetupLevelText(int fromLevelPoints, int deltaPoints)
        {
            _viewLevel.SetText(fromLevelPoints, deltaPoints).Forget();
        }
    }
}