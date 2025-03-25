using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class PlayerCollisions : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _logsLayer;

        [SerializeField]
        private LayerMask _carsLayer;

        private LayerMask _waterLayer = 4;


        private void OnCollisionEnter(Collision collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            Debug.Log($"collision with {collision.gameObject.name} {collisionLayer}");

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
                //transform.SetParent(_defaultParent);
            }
        }
    }
}