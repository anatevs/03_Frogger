using Cysharp.Threading.Tasks;
using GameManagement;
using System;
using System.Threading;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class FrogFriend : MonoBehaviour,
        IRoundEndListener
    {
        public bool IsAtPlayer => _isAtPlayer;

        [SerializeField]
        private Transform _playerArmature;

        [SerializeField]
        private GameObject _playerGO;

        [SerializeField]
        private OnSceneLogsService _activeLogs;

        private Transform _defaultParent;

        private CancellationTokenSource _ctn;

        [SerializeField]
        private FrogFriendData _data;

        private bool _isAtPlayer;

        public void Init()//(FrogFriendData data)
        {
            _defaultParent = transform.parent;

            //_data = data;

            AppearFriend().Forget();
        }

        public void OnEndRound()
        {
            gameObject.SetActive(false);
            transform.parent = _defaultParent;
        }

        public void FollowPlayer()
        {
            _ctn.Cancel();

            transform.parent = _playerArmature;

            transform.localPosition = Vector3.zero;

            _isAtPlayer = true;
        }


        public async UniTaskVoid AppearFriend()
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