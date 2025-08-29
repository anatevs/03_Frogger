using DG.Tweening;
using UnityEngine;

namespace GameCore
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _deathGO;

        [SerializeField]
        private GameObject _playerView;

        [SerializeField]
        private float _deathAnimScale = 3f;

        [SerializeField]
        private float _deathHalfDuration = 0.5f;

        public void ShowFrog(bool isShow)
        {
            _playerView.SetActive(isShow);
        }

        public Sequence DeathAnimation()
        {
            transform.rotation = Quaternion.identity;

            _deathGO.SetActive(true);

            var sequence = DOTween.Sequence().Pause();

            sequence.Append(
                _deathGO.transform.DOScale(_deathAnimScale, _deathHalfDuration)
                .Pause());

            sequence.Append(
                _deathGO.transform.DOScale(1, _deathHalfDuration)
                .OnComplete(MakeOnEnd)
                .Pause());

            return sequence;
        }

        private void MakeOnEnd()
        {
            _deathGO.SetActive(false);
        }
    }
}