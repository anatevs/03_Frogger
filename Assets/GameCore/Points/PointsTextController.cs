using UI;
using UnityEngine;

namespace GameCore
{
    public class PointsTextController : MonoBehaviour
    {
        private PointsView _view;

        private PointsStorage _storage;

        private void OnEnable()
        {
            _storage.OnPointsChanged += SetupText;
        }

        private void OnDisable()
        {
            _storage.OnPointsChanged -= SetupText;
        }

        public void SetupText(int points)
        {
            _view.SetText(points.ToString());
        }
    }
}