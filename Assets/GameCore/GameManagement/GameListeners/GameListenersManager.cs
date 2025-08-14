using System.Collections.Generic;
using VContainer.Unity;

namespace GameManagement
{
    public sealed class GameListenersManager :
        ITickable
    {
        private readonly List<IGameListener> _gameListeners = new();

        private readonly List<IStartGameListener> _startGameListeners = new();

        private readonly List<IUpdateListener> _updateListeners = new();

        private readonly List<IDamageListener> _restartRoundListeners = new();

        private readonly List<IRoundEndListener> _roundEndListeners = new();

        private readonly List<ILevelEndListener> _levelEndListeners = new();

        private readonly List<IGameEndListener> _gameEndListeners = new();

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

            if (listener is IUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (listener is IDamageListener restartRoundListener)
            {
                _restartRoundListeners.Add(restartRoundListener);
            }

            if (listener is IRoundEndListener roundEndListener)
            {
                _roundEndListeners.Add(roundEndListener);
            }

            if (listener is ILevelEndListener levelEndListener)
            {
                _levelEndListeners.Add(levelEndListener);
            }

            if (listener is IGameEndListener gameEndListener)
            {
                _gameEndListeners.Add(gameEndListener);
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

        public void RestartRound()
        {
            foreach (var listener in _restartRoundListeners)
            {
                listener.OnDamage();
            }
        }

        public void EndRound()
        {
            foreach (var listener in _roundEndListeners)
            {
                listener.OnEndRound();
            }
        }

        public void EndLevel()
        {
            foreach (var listener in _levelEndListeners)
            {
                listener.OnEndLevel();
            }
        }

        public void EndGame()
        {
            foreach (var listener in _gameEndListeners)
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

        void ITickable.Tick()
        {
            UpdateGame();
        }

        private void UpdateGame()
        {
            foreach (var listener in _updateListeners)
            {
                listener.OnUpdate();
            }
        }
    }
}