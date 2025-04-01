using UnityEngine;

namespace GameCore
{
    public sealed class FrogAnimation : MonoBehaviour
    {
        public float StartJumpDelay => _startJumpDelay;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _maxFrame = 21;

        [SerializeField]
        private float _startJumpFrame;

        private float _jumpSpeed = 1f;

        private float _startJumpDelay;

        private const string JUMP_TRIGGER = "Jump";

        private const string SPEED_PARAMETER = "AnimationSpeed";

        private const string JUMP_CLIP = "FrogArmature_Frog_Jump";

        public void SetupJumpSpeed(float jumpDuration)
        {
            var jumpClip = GetAnimationClip(JUMP_CLIP);

            _jumpSpeed = jumpClip.length / jumpDuration;

            _startJumpDelay = _startJumpFrame / _maxFrame * jumpDuration;
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