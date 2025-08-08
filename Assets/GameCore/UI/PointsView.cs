using TMPro;
using UnityEngine;

namespace UI
{
    public class PointsView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _pointsText;

        [SerializeField]
        private float _animDuration;

        public void SetText(string value)
        {
            _pointsText.text = value;
        }
    }
}