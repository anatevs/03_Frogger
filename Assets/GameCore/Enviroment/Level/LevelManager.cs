using GameManagement;
using UnityEngine;

namespace GameCore
{
    public sealed class LevelManager :
        IStartGameListener,
        ILevelEndListener,
        ILevelStartListener
    {
        private readonly LevelConfig[] _levelConfigs;

        private readonly PlayerLifes _playerLifes;

        private readonly RowsManager _rowsManager;

        private readonly WinPlaces _winPlaces;

        private readonly FrogFriend _frogFriend;

        private readonly GameListenersManager _listenersManager;

        private int _currentLevelIndex;

        public LevelManager(LevelConfig[] levelConfigs,
            PlayerLifes playerLifes,
            RowsManager rowsManager,
            WinPlaces winPlaces,
            FrogFriend frogFriend,
            GameListenersManager listenersManager)
        {
            _levelConfigs = levelConfigs;
            _playerLifes = playerLifes;
            _rowsManager = rowsManager;
            _winPlaces = winPlaces;
            _frogFriend = frogFriend;
            _listenersManager = listenersManager;
        }

        public void OnStartGame()
        {
            _currentLevelIndex = 0;

            var config = _levelConfigs[_currentLevelIndex];

            _frogFriend.EnableActiveLogs();

            _playerLifes.SetLifes(config.Lifes);

            _rowsManager.InitLevelRows(config.RowData);

            _winPlaces.SetupWinPlaces(config.WinPlaceGOData);

            _frogFriend.StartInit();
        }

        public void OnEndLevel()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex >= _levelConfigs.Length)
            {
                _listenersManager.EndGame();

                _currentLevelIndex = 0;

                Debug.Log("All levels completed, restarting from the first level.");

                return;
            }
        }

        public void OnStartLevel()
        {
            var config = _levelConfigs[_currentLevelIndex];

            _playerLifes.SetLifes(config.Lifes);

            _frogFriend.SetupLevel();

            _rowsManager.SetupLevelRows(config.RowData);

            _winPlaces.SetupWinPlaces(config.WinPlaceGOData);
        }
    }
}