using GameManagement;
using UnityEngine;

namespace GameCore
{
    public class LevelManager :
        IStartGameListener,
        ILevelEndListener
    {
        private readonly LevelConfig[] _levelConfigs;

        private readonly RowsManager _rowsManager;

        private readonly WinPlaces _winPlaces;

        private readonly FrogFriend _frogFriend;

        private int _currentLevelIndex;

        public LevelManager(LevelConfig[] levelConfigs,
            RowsManager rowsManager,
            WinPlaces winPlaces,
            FrogFriend frogFriend)
        {
            _levelConfigs = levelConfigs;
            _rowsManager = rowsManager;
            _winPlaces = winPlaces;
            _frogFriend = frogFriend;
        }

        public void OnStartGame()
        {
            _currentLevelIndex = 0;

            var cnf = _levelConfigs[_currentLevelIndex];

            _frogFriend.EnableActiveLogs();

            _rowsManager.InitLevelRows(cnf.RowData);

            _winPlaces.SetupWinPlaces(cnf.WinPlaceGOData);

            _frogFriend.StartInit();
        }

        public void OnEndLevel()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex >= _levelConfigs.Length)
            {
                _currentLevelIndex = 0;

                Debug.Log("All levels completed, restarting from the first level.");

                return;
            }

            var cnf = _levelConfigs[_currentLevelIndex];

            _rowsManager.SetupLevelRows(cnf.RowData);

            _winPlaces.SetupWinPlaces(cnf.WinPlaceGOData);

            _frogFriend.SetupLevel();
        }
    }
}