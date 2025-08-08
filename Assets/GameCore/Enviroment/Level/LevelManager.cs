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

        private int _currentLevelIndex;

        public LevelManager(LevelConfig[] levelConfigs,
            RowsManager rowsManager,
            WinPlaces winPlaces)
        {
            _levelConfigs = levelConfigs;
            _rowsManager = rowsManager;
            _winPlaces = winPlaces;
        }

        public void OnStartGame()
        {
            _currentLevelIndex = 0;

            var cnf = _levelConfigs[_currentLevelIndex];

            _rowsManager.InitLevelRows(cnf.RowData);

            _winPlaces.SetupWinPlaces(cnf.WinPlaceGOData);
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
        }
    }
}