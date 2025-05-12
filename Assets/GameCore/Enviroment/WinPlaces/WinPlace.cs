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

        private int _id;

        private LayerMask _defaultLayer = 0;

        private PlayerJump _playerJump;

        public void SetAchieved(bool isAcheved)
        {
            _achevedView.SetActive(isAcheved);

            if (isAcheved)
            {
                gameObject.layer = _wallLayer;

                return;
            }

            gameObject.layer = _defaultLayer;
        }

        private void Awake()
        {
            _id = (int)transform.position.x;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerJump>(out _playerJump))
            {
                _playerJump.OnJumpEnd += MakeOnAchieved;
            }
        }

        private void MakeOnAchieved()
        {
            SetAchieved(true);

            OnAchieved?.Invoke(_id);

            _playerJump.OnJumpEnd -= MakeOnAchieved;
        }
    }
}