using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _text;

        public void SetValue(int value)
        {
            _slider.value = value;

            _text.text = value.ToString();
        }
    }
}