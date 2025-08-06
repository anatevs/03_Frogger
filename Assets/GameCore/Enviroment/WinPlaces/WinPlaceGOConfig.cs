using DG.Tweening;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "WinPlaceGO",
        menuName = "Configs/WinPlaceGO")]
    public class WinPlaceGOConfig : ScriptableObject
    {
        [SerializeField]
        private GameObject _placeGO;

        [SerializeField]
        private bool _isEnemy;

        [SerializeField]
        private float[] _zPoints = new float[2];// { 8.8f, 7.36f };

        [SerializeField]
        private float _setDuration = 0.15f;

        [SerializeField]
        private float _stayDuration = 0.5f;

        public (Sequence, bool) Show(float xPos)
        {
            _placeGO.transform.position = new (xPos, 0, _zPoints[0]);

            var sequence = DOTween.Sequence().Pause();

            sequence
                .Append(_placeGO.transform.DOMoveZ(_zPoints[1], _setDuration))
                .AppendInterval(_stayDuration)
                .Append(_placeGO.transform.DOMoveZ(_zPoints[0], _stayDuration));

            return (sequence, _isEnemy);
        }
    }
}