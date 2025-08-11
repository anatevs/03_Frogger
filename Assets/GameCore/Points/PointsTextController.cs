using UI;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class PointsTextController : MonoBehaviour
    {
        [SerializeField]
        private PointsView _view;

        private PointsStorage _storage;

        [Inject]
        public void Construct(PointsStorage storage)
        {
            _storage = storage;
        }

        private void OnEnable()
        {
            _view.SetText(_storage.Value.ToString());

            _storage.OnPointsChanged += SetupText;
        }

        private void OnDisable()
        {
            _storage.OnPointsChanged -= SetupText;
        }

        public void SetupText(int startPoints, int deltaPoints)
        {
            _view.SetText(startPoints, deltaPoints).Forget();
        }
    }
}