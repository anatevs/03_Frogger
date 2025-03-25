using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class LogController : MonoBehaviour
    {
        [SerializeField]
        private Transform _view;

        private float _speed;

        private Vector3 _direction = Vector3.left;

        public void Init(float speed, Vector3 direction, float length)
        {
            _speed = speed;
            _direction = direction;

            SetLength(length);
        }

        private void SetLength(float length)
        {
            _view.localScale = new Vector3(length / _view.localScale.x, 1, 1);
        }

        private void Update()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

    }
}