using GameManagement;

namespace GameCore
{
    public sealed class PointsStorageManager :
        IRoundStartListener,
        ILevelStartListener,
        IRoundRestartListener,
        ILevelRestartListener
    {
        public PointsStorage LevelStorage => _levelStorage;

        public PointsStorage TotalStorage => _totalStorage;

        public int BestScore => _bestScore;

        private PointsStorage _totalStorage;

        private PointsStorage _levelStorage;

        private int _bestScore = 0;

        private int _roundPoints = 0;


        public PointsStorageManager()
        {
            _totalStorage = new PointsStorage();

            _levelStorage = new PointsStorage();
        }

        public void SetupStorages(int total, int level, int bestScore)
        {
            _totalStorage.ChangeValue(total);

            _levelStorage.ChangeValue(level);

            _bestScore = bestScore;
        }

        public void ChangeValue(int amount)
        {
            _totalStorage.ChangeValue(amount);

            _levelStorage.ChangeValue(amount);

            _roundPoints += amount;
        }

        public void OnRestartRound()
        {
            ChangeValue(-_roundPoints);
        }

        public void OnStartRound()
        {
            _roundPoints = 0;
        }

        public void OnRestartLevel()
        {
            ChangeValue(-_levelStorage.Value);
        }

        public void OnStartLevel()
        {
            _levelStorage.ChangeValue(-_levelStorage.Value);
        }
    }
}