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

        private readonly List<IPauseListener> _pauseListeners = new();

        private readonly List<IResumeListener> _resumeListeners = new();

        private readonly List<IRoundRestartListener> _roundRestartListeners = new();

        private readonly List<IRoundEndListener> _roundEndListeners = new();

        private readonly List<IRoundStartListener> _roundStartListeners = new();

        private readonly List<ILevelEndListener> _levelEndListeners = new();

        private readonly List<ILevelRestartListener> _levelRestartListeners = new();

        private readonly List<ILevelStartListener> _levelStartListeners = new();

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

            if (listener is IPauseListener pauseListener)
            {
                _pauseListeners.Add(pauseListener);
            }

            if (listener is IResumeListener resumeListener)
            {
                _resumeListeners.Add(resumeListener);
            }

            if (listener is IRoundRestartListener restartRoundListener)
            {
                _roundRestartListeners.Add(restartRoundListener);
            }

            if (listener is IRoundEndListener roundEndListener)
            {
                _roundEndListeners.Add(roundEndListener);
            }

            if (listener is IRoundStartListener roundStartListener)
            {
                _roundStartListeners.Add(roundStartListener);
            }

            if (listener is ILevelEndListener levelEndListener)
            {
                _levelEndListeners.Add(levelEndListener);
            }

            if (listener is ILevelRestartListener levelRestartListener)
            {
                _levelRestartListeners.Add(levelRestartListener);
            }

            if (listener is ILevelStartListener levelStartListener)
            {
                _levelStartListeners.Add(levelStartListener);
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

        public void PauseGame()
        {
            foreach (var listener in _pauseListeners)
            {
                listener.OnPause();
            }
        }

        public void ResumeGame()
        {
            foreach (var listener in _resumeListeners)
            {
                listener.OnResume();
            }
        }

        public void RestartRound()
        {
            foreach (var listener in _roundRestartListeners)
            {
                listener.OnRestartRound();
            }
        }

        public void EndRound()
        {
            foreach (var listener in _roundEndListeners)
            {
                listener.OnEndRound();
            }
        }

        public void StartRound()
        {
            foreach (var listener in _roundStartListeners)
            {
                listener.OnStartRound();
            }
        }

        public void EndLevel()
        {
            foreach (var listener in _levelEndListeners)
            {
                listener.OnEndLevel();
            }
        }

        public void RestartLevel()
        {
            foreach (var listener in _levelRestartListeners)
            {
                listener.OnRestartLevel();
            }
        }

        public void StartLevel()
        {
            foreach (var listener in _levelStartListeners)
            {
                listener.OnStartLevel();
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