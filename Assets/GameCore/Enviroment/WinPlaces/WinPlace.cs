using DG.Tweening;
using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class WinPlace : MonoBehaviour
    {
        public event Action<int> OnAchieved;

        public int Id => _id;

        [SerializeField]
        private int _wallLayer;

        [SerializeField]
        private GameObject _achevedView;

        [SerializeField]
        private float _viewAnimHalfDuration = 0.15f;

        [SerializeField]
        private float _viewAnimScale = 1.2f;

        private readonly Vector3[] _viewScales = new Vector3[2];

        private int _id;

        private LayerMask _defaultLayer = 0;

        private PlayerJump _playerJump;

        private bool _containGO;

        private bool _isDanger;

        public void SetAchieved(bool isAcheved)
        {
            _achevedView.SetActive(isAcheved);
        }

        public void PlaceGO(Sequence sequence, bool isEnemy)
        {
            if (!_containGO)
            {
                _containGO = true;

                _isDanger = isEnemy;

                sequence.OnComplete(() => {
                    _containGO = false;
                    _isDanger = false;
                });

                sequence.Play();
            }
        }

        private void Awake()
        {
            _id = (int)transform.position.x;

            _viewScales[0] = _achevedView.transform.localScale;
            _viewScales[1] = _achevedView.transform.localScale * _viewAnimScale;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isDanger)
            {
                if (other.gameObject.TryGetComponent<PlayerJump>(out _playerJump))
                {
                    _playerJump.OnJumpEnd += MakeOnAchieved;
                }
            }
        }

        private void MakeOnAchieved()
        {
            SetAchieved(true);

            AnimateAchievedView();

            OnAchieved?.Invoke(_id);

            _playerJump.OnJumpEnd -= MakeOnAchieved;
        }

        private void AnimateAchievedView()
        {
            DOTween.Sequence()
                .Append(_achevedView.transform.DOScale(_viewScales[1], _viewAnimHalfDuration))
                .Append(_achevedView.transform.DOScale(_viewScales[0], _viewAnimHalfDuration));
        }
    }
}