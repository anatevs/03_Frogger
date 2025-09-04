using DG.Tweening;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "WinPlaceGO",
        menuName = "Configs/WinPlaceGO")]
    public sealed class WinPlaceGOConfig : ScriptableObject
    {
        public GameObject Prefab => _prefab;

        public float ActiveDuration => _stayDuration + 2 * _setDuration;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private bool _isEnemy;

        [SerializeField]
        private float[] _zPoints = { 8.8f, 7.36f };

        [SerializeField]
        private float _setDuration = 0.4f;

        [SerializeField]
        private float _stayDuration = 2f;

        public (Sequence, bool) Show(Transform goTransform, float xPos)
        {
            goTransform.position = new (xPos, 0, _zPoints[0]);

            var sequence = DOTween.Sequence().Pause();

            sequence
                .Append(goTransform.DOMoveZ(_zPoints[1], _setDuration).SetLink(goTransform.gameObject))
                .AppendInterval(_stayDuration).SetLink(goTransform.gameObject)
                .Append(goTransform.DOMoveZ(_zPoints[0], _setDuration).SetLink(goTransform.gameObject));

            return (sequence, _isEnemy);
        }
    }
}