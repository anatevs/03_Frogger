using System;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Player : MonoBehaviour
    {
        public event Action<Transform> OnWin;

        public event Action OnDropped;

        public event Action OnCarHit;

        [SerializeField]
        private FrogAnimation _frogAnimation;

        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _jumpDuration;

        [SerializeField]
        private Transform _defaultParent;

        [SerializeField]
        private LayerMask _logsLayer;

        [SerializeField]
        private LayerMask _carsLayer;

        [SerializeField]
        private LayerMask _wallLayer;

        [SerializeField]
        private Transform _body;

        private LayerMask _waterLayer = 1 << 4;

        private void OnCollisionEnter(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _logsLayer.value) > 0)
            {
                transform.SetParent(collision.transform);
            }

            if ((collisionLayer & _carsLayer.value) > 0)
            {
                Debug.Log("car collision");

                return;
            }

            if ((collisionLayer & _waterLayer) > 0)
            {
                Debug.Log("water collision");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _logsLayer.value) > 0)
            {
                transform.SetParent(_defaultParent);
            }
        }
    }
}