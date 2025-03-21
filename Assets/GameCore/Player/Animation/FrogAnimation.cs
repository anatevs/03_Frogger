using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class FrogAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private float _jumpSpeed = 1f;

        private const string JUMP_TRIGGER = "Jump";

        private const string SPEED_PARAMETER = "AnimationSpeed";

        private const string JUMP_CLIP = "FrogArmature_Frog_Jump";


        public void SetupJumpSpeed(float jumpDuration)
        {
            var jumpClip = GetAnimationClip(JUMP_CLIP);

            _jumpSpeed = jumpClip.length / jumpDuration;
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