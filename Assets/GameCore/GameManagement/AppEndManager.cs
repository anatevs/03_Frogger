using System;
using UnityEngine;

namespace GameManagement
{
    public class AppEndManager : MonoBehaviour
    {
        public event Action OnStopped;

        private bool _isStopped;

#if UNITY_ANDROID
        private void OnApplicationPause(bool pause)
        {
            Debug.Log($"app pause {pause}");
            MakeEndApp(pause);
        }
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        private void OnApplicationFocus(bool focus)
        {
            Debug.Log($"app focus {focus}");
            MakeEndApp(!focus);
        }
#endif

        private void OnApplicationQuit()
        {
            Debug.Log("app quit");

            if (!_isStopped)
            {
                MakeEndApp(true);
            }
        }

        private void MakeEndApp(bool isStopped)
        {
            _isStopped = isStopped;

            OnStopped?.Invoke();
        }

    }
}