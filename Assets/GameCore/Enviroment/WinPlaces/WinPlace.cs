using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class WinPlace : MonoBehaviour
    {
        public event Action<int> OnAchieved;

        public int Id => _id;

        [SerializeField]
        private int _damageLayer;

        [SerializeField]
        private GameObject _achevedView;

        private float _achieveAnimHalfDuration = 0.15f;

        private float _achieveAnimScale = 1.2f;

        private readonly Vector3[] _viewScales = new Vector3[2];

        private int _id;

        private int _defaultLayer;

        private PlayerJump _playerJump;

        private bool _containGO;

        private bool _isDanger;

        public void SetAchieved(bool isAcheved)
        {
            _achevedView.SetActive(isAcheved);
        }

        public async UniTask PlaceGO(Sequence sequence, bool isEnemy, CancellationToken token)
        {
            if (!_containGO)
            {
                _containGO = true;

                _isDanger = isEnemy;

                if (isEnemy)
                {
                    gameObject.layer = _damageLayer;
                }

                await sequence.Play().OnComplete(() => {
                    _containGO = false;
                    _isDanger = false;
                    gameObject.layer = _defaultLayer;
                })
                    .WithCancellation(token);
            }
        }

        private void Awake()
        {
            _id = (int)transform.position.x;

            _viewScales[0] = _achevedView.transform.localScale;
            _viewScales[1] = _achevedView.transform.localScale * _achieveAnimScale;

            _defaultLayer = gameObject.layer;
        }

        public void OnPlayerTriggered(PlayerJump playerJump)
        {
            if (!_isDanger)
            {
                _playerJump = playerJump;

                _playerJump.OnJumpEnd += MakeOnAchieved;
            }
        }

        private void MakeOnAchieved()
        {
            SetAchieved(true);

            var sequence = AnimateAchievedView();

            sequence
                .OnStart(() => _playerJump.gameObject.SetActive(false))
                .OnComplete(() =>
                {
                    OnAchieved?.Invoke(_id);
                    _playerJump.gameObject.SetActive(true);
                })
                .Play();

            _playerJump.OnJumpEnd -= MakeOnAchieved;
        }

        private Sequence AnimateAchievedView()
        {
            return DOTween.Sequence().Pause()
                .Append(_achevedView.transform.DOScale(_viewScales[1], _achieveAnimHalfDuration))
                .Append(_achevedView.transform.DOScale(_viewScales[0], _achieveAnimHalfDuration));
        }
    }
}