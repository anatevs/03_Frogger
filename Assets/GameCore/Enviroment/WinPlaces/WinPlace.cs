using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class WinPlace : MonoBehaviour
    {
        public event Action<int> OnAchieveStarted;

        public event Action OnAchieveEnded;

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

                await sequence.Play().OnComplete(() =>
                {
                    _containGO = false;
                    _isDanger = false;
                    gameObject.layer = _defaultLayer;
                })
                    .AsyncWaitForCompletion();
            }
            else
            {
                sequence.Kill();

                await UniTask.Yield();
            }
        }

        private void Awake()
        {
            _viewScales[0] = _achevedView.transform.localScale;
            _viewScales[1] = _achevedView.transform.localScale * _achieveAnimScale;

            _defaultLayer = gameObject.layer;
        }

        public void Init(PlayerJump playerJump)
        {
            _id = (int)transform.position.x;
            _playerJump = playerJump;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerJump.gameObject)
            {
                if (!_isDanger)
                {
                    _playerJump.OnJumpEnd += MakeOnAchieved;
                }
            }
        }

        private void MakeOnAchieved()
        {
            SetAchieved(true);

            var sequence = AnimateAchievedView();

            sequence
                .OnStart(() =>
                {
                    _playerJump.gameObject.SetActive(false);
                    OnAchieveStarted?.Invoke(_id);
                })
                .OnComplete(() =>
                {
                    OnAchieveEnded?.Invoke();
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