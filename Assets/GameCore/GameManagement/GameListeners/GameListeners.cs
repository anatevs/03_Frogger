namespace GameManagement
{
    public interface IGameListener
    {
    }

    public interface IStartGameListener : IGameListener
    {
        public void StartGame();
    }

    public interface IEndRoundListener : IGameListener
    {
        public void EndRound();
    }

    public interface IEndGameListener : IGameListener
    {
        public void EndGame();
    }

    public interface IAppQuitListener : IGameListener
    {
        public void OnAppQuit();
    }
}