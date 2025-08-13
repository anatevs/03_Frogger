namespace GameManagement
{
    public interface IGameListener
    {
    }

    public interface IStartGameListener : IGameListener
    {
        public void OnStartGame();
    }

    public interface IUpdateListener : IGameListener
    {
        public void OnUpdate();
    }

    public interface IRestartRoundListener : IGameListener
    {
        public void OnRestartRound();
    }

    public interface IRoundEndListener : IGameListener
    {
        public void OnEndRound();
    }

    public interface ILevelEndListener : IGameListener
    {
        public void OnEndLevel();
    }

    public interface IGameEndListener : IGameListener
    {
        public void OnEndGame();
    }

    public interface IAppQuitListener : IGameListener
    {
        public void OnAppQuit();
    }
}