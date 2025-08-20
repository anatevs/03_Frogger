using GameManagement;

namespace GameCore
{
    public class PointsStorageController :
        IRoundStartListener,
        ILevelStartListener
    {
        public PointsStorage LevelStorage => _levelStorage;

        public PointsStorage TotalStorage => _totalStorage;

        private PointsStorage _totalStorage;

        private PointsStorage _levelStorage;

        private int _roundPoints = 0;


        public PointsStorageController()
        {
            _totalStorage = new PointsStorage();

            _levelStorage = new PointsStorage();
        }

        public void ChangeValue(int amount)
        {
            _totalStorage.ChangeValue(amount);

            _levelStorage.ChangeValue(amount);

            _roundPoints += amount;
        }

        public void OnDamage()
        {
            ChangeValue(-_roundPoints);
        }

        public void OnStartRound()
        {
            _roundPoints = 0;
        }

        public void OnStartLevel()
        {
            _levelStorage.ChangeValue(-_levelStorage.Value);
        }
    }
}