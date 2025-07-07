using System.Collections.Generic;

namespace GameManagement
{
    public sealed class GameListenersManager
    {
        private readonly List<IGameListener> _gameListeners = new();

        private readonly List<IStartGameListener> _startGameListeners = new();

        private readonly List<IEndRoundListener> _endRoundlListeners = new();

        private readonly List<IEndLevelListener> _endLevelListeners = new();

        private readonly List<IEndGameListener> _endGameListeners = new();

        private readonly List<IAppQuitListener> _appQuitListeners = new();

        public void AddListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (IGameListener listener in listeners)
            {
                AddListener(listener);
            }
        }

        public void AddListener(IGameListener listener)
        {
            _gameListeners.Add(listener);

            if (listener is IStartGameListener startListener)
            {
                _startGameListeners.Add(startListener);
            }

            if (listener is IEndRoundListener endRoundListener)
            {
                _endRoundlListeners.Add(endRoundListener);
            }

            if (listener is IEndGameListener endLevelListener)
            {
                _endGameListeners.Add(endLevelListener);
            }

            if (listener is IEndGameListener endGameListener)
            {
                _endGameListeners.Add(endGameListener);
            }

            if (listener is IAppQuitListener quitListener)
            {
                _appQuitListeners.Add(quitListener);
            }
        }

        public void StartGame()
        {
            foreach (var listener in _startGameListeners)
            {
                listener.OnStartGame();
            }
        }

        public void EndRound()
        {
            foreach (var listener in _endRoundlListeners)
            {
                listener.OnEndRound();
            }
        }

        public void EndLevel()
        {
            foreach (var listener in _endLevelListeners)
            {
                listener.OnEndLevel();
            }
        }

        public void EndGame()
        {
            foreach (var listener in _endGameListeners)
            {
                listener.OnEndGame();
            }
        }

        public void AppQuit()
        {
            foreach (var listener in _appQuitListeners)
            {
                listener.OnAppQuit();
            }
        }
    }
}