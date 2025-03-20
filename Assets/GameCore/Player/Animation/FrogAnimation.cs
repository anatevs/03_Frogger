using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class FrogAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _jumpDuration = 0.5f;

        private const string JUMP_TRIGGER = "Jump";

        private const string SPEED_PARAMETER = "AnimationSpeed";

        private const string JUMP_CLIP = "FrogArmature_Frog_Jump";

        private float _jumpSpeed;

        private void Awake()
        {
            var jumpClip = GetAnimationClip(JUMP_CLIP);

            _jumpSpeed = jumpClip.length / _jumpDuration;
        }

        public void Jump()
        {
            _animator.SetFloat(SPEED_PARAMETER, _jumpSpeed);
            _animator.SetTrigger(JUMP_TRIGGER);
        }

        private AnimationClip GetAnimationClip(string clipName)
        {
            var clips = _animator.runtimeAnimatorController.animationClips;

            foreach (var cl in clips)
            {
                if (cl.name == clipName)
                {
                    return cl;
                }
            }

            Debug.LogError($"there is no clip with name {clipName} in the animator {_animator}");
            return null;
        }
    }
}