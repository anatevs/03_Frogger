using UI;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class PointsText : MonoBehaviour
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

            _storage.OnChanged += SetupTotalText;
        }

        private void OnDisable()
        {
            _storage.OnChanged -= SetupTotalText;
        }

        public void SetupTotalText(int fromPoints, int deltaPoints)
        {
            _view.SetText(fromPoints, deltaPoints).Forget();
        }
    }
}