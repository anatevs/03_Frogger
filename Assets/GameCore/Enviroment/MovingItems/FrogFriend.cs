using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public class FrogFriend : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerArmature;

        [SerializeField]
        private GameObject _playerGO;

        private Transform _defaultParent;

        private CancellationTokenSource _ctn;

        private void Awake()
        {
            _defaultParent = transform.parent;
        }

        public void Init()
        {
            _defaultParent = transform.parent;
        }

        public void FollowPlayer()
        {
            _ctn.Cancel();
            transform.parent = _playerArmature;
            transform.localPosition = Vector3.zero;
        }

        public async UniTaskVoid FollowLog(Transform logTransform, float atLogXPos, float duration, CancellationToken token)
        {
            transform.gameObject.SetActive(true);

            transform.parent = logTransform;

            transform.localPosition = Vector3.right * atLogXPos;

            await UniTask.WaitForSeconds(duration, cancellationToken: token);

            transform.gameObject.SetActive(false);
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
    public struct FriendData
    {

    }
}