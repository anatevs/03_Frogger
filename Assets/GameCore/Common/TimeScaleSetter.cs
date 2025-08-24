using GameManagement;
using UnityEngine;

namespace GameCore
{
    public class TimeScaleSetter :
        IPauseListener,
        IResumeListener,
        IStartGameListener,
        IGameEndListener

    {
        public void OnStartGame()
        {
            Time.timeScale = 1;
        }

        public void OnEndGame()
        {
            Time.timeScale = 0;
        }

        public void OnPause()
        {
            Time.timeScale = 0;
        }

        public void OnResume()
        {
            Time.timeScale = 1;
        }
    }
}