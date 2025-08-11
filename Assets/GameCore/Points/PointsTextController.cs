using UI;
using UnityEngine;

namespace GameCore
{
    public class PointsTextController : MonoBehaviour
    {
        [SerializeField]
        private PointsView _view;

        [SerializeField]
        private PointsStorage _storage;

        private void OnEnable()
        {
            _view.SetText(_storage.Value.ToString());

            _storage.OnPointsChanged += SetupText;
        }

        private void OnDisable()
        {
            _storage.OnPointsChanged -= SetupText;
        }

        public void SetupText(int deltaPoints)
        {
            _view.SetText(_storage.Value - deltaPoints, deltaPoints).Forget();
        }
    }
}