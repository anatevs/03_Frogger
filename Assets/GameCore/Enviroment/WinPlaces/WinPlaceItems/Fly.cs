using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class Fly : MonoBehaviour
    {
        private GameObject _playerGO;

        private PointsCounter _pointsCounter;

        public void Init(GameObject playerGO,
            PointsCounter pointsCounter)
        {
            _playerGO = playerGO;

            _pointsCounter = pointsCounter;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerGO)
            {
                _pointsCounter.AddExtraPoints();

                gameObject.SetActive(false);
            }
        }
    }
}