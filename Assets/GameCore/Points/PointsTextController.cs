using UI;

namespace GameCore
{
    public class PointsTextController
    {
        private readonly PointsView _view;

        private readonly PointsStorage _storage;

        public PointsTextController(PointsView view,
            PointsStorage storage)
        {
            _view = view;
            _storage = storage;
        }

        public void Enable()
        {
            _view.SetText(_storage.Value.ToString());

            _storage.OnChanged += SetupTotalText;
        }

        public void Disable()
        {
            _storage.OnChanged -= SetupTotalText;
        }

        public void SetupTotalText(int fromPoints, int deltaPoints)
        {
            _view.SetText(fromPoints, deltaPoints).Forget();
        }
    }
}