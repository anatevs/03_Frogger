using DG.Tweening;
using System;
using UnityEngine;

namespace GameCore
{
    public class DeathAnimation : MonoBehaviour
    {
        public event Action OnAnimationEnded;

        [SerializeField]
        private GameObject _deathImage;

        [SerializeField]
        private GameObject _playerView;

        [SerializeField]
        private float _animScale = 3f;

        [SerializeField]
        private float _halfDuration = 0.5f;

        public bool IsPlay;
        private void Update()
        {
            if (IsPlay)
            {
                IsPlay = false;
                PlayAnimation();
            }
        }

        public void PlayAnimation()
        {
            _playerView.SetActive(false);
            _deathImage.SetActive(true);

            var sequence = DOTween.Sequence().Pause();

            sequence.Append(
                _deathImage.transform.DOScale(_animScale, _halfDuration)
                .Pause());

            sequence.Append(
                _deathImage.transform.DOScale(1, _halfDuration)
                .OnComplete(MakeOnEnd)
                .Pause());

            sequence.Play();
        }

        private void MakeOnEnd()
        {
            Debug.Log("end anim");

            _playerView.SetActive(true);
            _deathImage.SetActive(false);

            OnAnimationEnded?.Invoke();
        }
    }
}