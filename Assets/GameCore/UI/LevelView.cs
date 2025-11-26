using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _levelText;

        private readonly string _title = "Level-";

        public void SetLevelNumber(int levelNumber)
        {
            _levelText.text = $"{_title}{levelNumber}";
        }
    }
}