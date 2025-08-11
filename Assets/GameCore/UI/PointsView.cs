using Cysharp.Threading.Tasks;
using System;
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

        public async UniTaskVoid SetText(int startValue, int deltaValue)
        {
            var current = startValue;
            var end = startValue + deltaValue;

            var unitsDuration = _animDuration / deltaValue;

            var sign = Math.Sign(deltaValue);

            while ((end - current) * sign > 0)
            {
                var deltaCount = (int) ((Time.deltaTime / unitsDuration) + sign);

                current += deltaCount;

                SetText(current.ToString());

                await UniTask.Yield();
            }

            SetText(end.ToString());
        }
    }
}