using Cysharp.Threading.Tasks;
using GameManagement;
using System;
using System.Threading;
using UnityEngine;
using VContainer;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class FrogFriend : MonoBehaviour,
        IRoundEndListener,
        IDamageListener,
        IDisposable
    {
        [SerializeField]
        private Transform _playerArmature;

        [SerializeField]
        private GameObject _playerGO;

        [SerializeField]
        private PoolsService _poolsService;

        [SerializeField]
        private FrogFriendData _data;

        private PointsCounter _pointsCounter;

        private ActiveLogsService _activeLogs;

        private Transform _defaultParent;

        private CancellationTokenSource _ctn;

        private bool _isAtPlayer;

        [Inject]
        public void Construct(PointsCounter pointsCounter)
        {
            _pointsCounter = pointsCounter;
        }

        void IDisposable.Dispose()
        {
            _activeLogs.Dispose();
        }

        public void EnableActiveLogs()
        {
            _activeLogs = new ActiveLogsService(_poolsService);
        }

        public void StartInit()//(FrogFriendData data)
        {
            _defaultParent = transform.parent;

            //_data = data;

            AppearFriendProcess().Forget();
        }

        public void SetupLevel()//(FrogFriendData data)
        {
            //_data = data;

            Reset();
        }

        public void OnEndRound()
        {
            if (_isAtPlayer)
            {
                Reset();
            }
        }

        public void OnDamage()
        {
            if (_isAtPlayer)
            {
                Reset();
            }
        }

        private void Reset()
        {
            _ctn.Cancel();

            _isAtPlayer = false;
            gameObject.SetActive(false);
            transform.parent = _defaultParent;

            AppearFriendProcess().Forget();
        }

        private async UniTaskVoid AppearFriendProcess()
        {
            _ctn = new CancellationTokenSource();

            await UniTask.WaitForSeconds(_data.Period, cancellationToken: _ctn.Token);

            while (!_ctn.IsCancellationRequested)
            {
                var index = UnityEngine.Random.Range(0, _activeLogs.Logs.Count);

                await FollowLog(_activeLogs.Logs[index], _ctn.Token);

                await UniTask.WaitForSeconds(_data.Period, cancellationToken: _ctn.Token);
            }
        }

        private async UniTask FollowLog(MovingItem log, CancellationToken token)
        {
            transform.gameObject.SetActive(true);

            transform.parent = log.transform;

            var atLogXPos = UnityEngine.Random.Range(-log.HalfX, log.HalfX);

            transform.localPosition = Vector3.right * atLogXPos;

            await UniTask.WaitForSeconds(_data.StayDuration, cancellationToken: token);

            transform.gameObject.SetActive(false);

            transform.parent = _defaultParent;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerGO)
            {
                FollowPlayer();
            }
        }

        private void FollowPlayer()
        {
            _ctn.Cancel();

            transform.rotation = _playerGO.transform.rotation;

            transform.parent = _playerArmature;

            transform.localPosition = Vector3.zero;

            _isAtPlayer = true;

            _pointsCounter.AddExtraPoints();
        }
    }

    [Serializable]
    public struct FrogFriendData
    {
        [field: SerializeField]
        public float StayDuration { get; private set; }

        [field: SerializeField]
        public float Period { get; private set; }
    }
}