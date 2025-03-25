using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class LogsManager : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private Vector3 _direction = Vector3.left;

        [SerializeField]
        private LogController logController;

        private void Start()
        {
            InitLogs();
        }

        private void InitLogs()
        {
            logController.Init(_speed, _direction, 1);
        }
    }
}