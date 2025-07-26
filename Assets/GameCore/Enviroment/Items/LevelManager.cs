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

        private int _currentLevelIndex;

        public LevelManager(LevelConfig[] levelConfigs, RowsManager rowsManager)
        {
            _levelConfigs = levelConfigs;
            _rowsManager = rowsManager;
        }

        public void OnStartGame()
        {
            _currentLevelIndex = 0;

            _rowsManager.InitLevelRows(_levelConfigs[_currentLevelIndex]);
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

            _rowsManager.SetupLevelRows(_levelConfigs[_currentLevelIndex]);
        }
    }
}