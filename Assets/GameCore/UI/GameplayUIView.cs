using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameplayUIView : MonoBehaviour
    {
        public event Action OnPauseClicked;

        [SerializeField]
        private Button _pauseButton;

        public void Show()
        {
            _pauseButton.onClick.AddListener(ClickPause);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _pauseButton.onClick.RemoveAllListeners();

            gameObject.SetActive(false);
        }

        public void ClickPause()
        {
            OnPauseClicked?.Invoke();
        }
    }
}