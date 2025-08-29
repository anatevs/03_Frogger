using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseMenuView : MonoBehaviour
    {
        public event Action OnResumeClicked;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private TMP_Text _bestScoreText;

        public void Show(string bestScore)
        {
            _resumeButton.onClick.AddListener(ClickResume);

            _bestScoreText.text = bestScore;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _resumeButton.onClick.RemoveAllListeners();

            gameObject.SetActive(false);
        }

        private void ClickResume()
        {
            OnResumeClicked?.Invoke();
        }

        //current info: completed levels, total score
    }
}