namespace GameManagement
{
    public interface IGameListener
    {
    }

    public interface IStartGameListener : IGameListener
    {
        public void OnStartGame();
    }

    public interface IEndRoundListener : IGameListener
    {
        public void OnEndRound();
    }

    public interface IEndLevelListener : IGameListener
    {
        public void OnEndLevel();
    }

    public interface IEndGameListener : IGameListener
    {
        public void OnEndGame();
    }

    public interface IAppQuitListener : IGameListener
    {
        public void OnAppQuit();
    }
}