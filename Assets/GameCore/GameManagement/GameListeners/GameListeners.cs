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

    public interface IPauseListener : IGameListener
    {
        public void OnPause();
    }

    public interface IResumeListener : IGameListener
    {
        public void OnResume();
    }

    public interface IDamageListener : IGameListener
    {
        public void OnDamage();
    }

    public interface IRoundEndListener : IGameListener
    {
        public void OnEndRound();
    }

    public interface IRoundStartListener : IGameListener
    {
        public void OnStartRound();
    }

    public interface ILevelEndListener : IGameListener
    {
        public void OnEndLevel();
    }

    public interface ILevelStartListener : IGameListener
    {
        public void OnStartLevel();
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