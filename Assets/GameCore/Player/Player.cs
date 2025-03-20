using GameCore;
using System.Collections;
using UnityEngine;

namespace Assets.GameCore.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private FrogAnimation _frogAnimation;

        [SerializeField]
        private InputHandler _inputHandler;

        private void OnEnable()
        {
            _inputHandler.OnMoved += MovePlayer;
        }

        private void OnDisable()
        {
            _inputHandler.OnMoved -= MovePlayer;
        }

        //private void Update()
        //{
        //    if (_inputHandler.IsMoving)
        //    {
        //        _frogAnimation.Jump();
        //    }
        //}

        private void MovePlayer(Vector3 direction)
        {
            transform.Translate(direction);

            _frogAnimation.Jump();
        }
    }
}