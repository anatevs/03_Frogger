using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuView : MonoBehaviour
    {
        public event Action OnResumeClicked;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private TMP_Text _bestScoreText;

        public void Show(string bestScore)
        {
            _bestScoreText.text = bestScore;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }


        //current info: completed levels, total score
    }
}