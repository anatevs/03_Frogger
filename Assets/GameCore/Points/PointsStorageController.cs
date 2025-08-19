namespace GameCore
{
    public class PointsStorageController
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

        public void OnEndRound()
        {
            _roundPoints = 0;
        }

        public void OnEndLevel()
        {
            _levelStorage.ChangeValue(-_levelStorage.Value);

            _roundPoints = 0;
        }
    }
}