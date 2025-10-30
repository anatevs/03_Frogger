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

        private Sequence _damageSeq;

        private void OnDisable()
        {
            if (_damageSeq != null && _damageSeq.IsActive())
            {
                _damageSeq.Kill();
            }
        }

        public void Show(bool isShow)
        {
            _playerView.SetActive(isShow);
        }

        public Sequence DeathAnimation()
        {
            transform.rotation = Quaternion.identity;

            _deathGO.SetActive(true);

            _damageSeq = DOTween.Sequence().Pause();

            _damageSeq.Append(
                _deathGO.transform.DOScale(_deathAnimScale, _deathHalfDuration)
                .Pause());

            _damageSeq.Append(
                _deathGO.transform.DOScale(1, _deathHalfDuration)
                .OnComplete(MakeOnEnd)
                .Pause());

            return _damageSeq;
        }

        private void MakeOnEnd()
        {
            _deathGO.SetActive(false);
        }
    }
}