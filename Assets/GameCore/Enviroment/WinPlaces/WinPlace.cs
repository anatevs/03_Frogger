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

        private void Awake()
        {
            _id = (int)transform.position.x;
        }

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                SetAchieved(true);

                OnAchieved?.Invoke(_id);

                player.SetToStart();
            }
        }
    }
}