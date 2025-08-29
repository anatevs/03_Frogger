using DG.Tweening;
using UnityEngine;

namespace GameCore
{
    public sealed class TurtleDiverView : MonoBehaviour
    {
        private readonly float _colliderDiveShift = -0.06f;

        private readonly float _defaultPosY = 0f;

        private readonly float[] _divePosY = {-0.13f, -0.2f };

        [SerializeField]
        private SpriteRenderer[] _circleSprites;

        
        private Vector3 _defaultColliderPos;

        private Vector3 _diveColliderPos;

        private BoxCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();

            _defaultColliderPos = _collider.center;

            _diveColliderPos = new Vector3(
                _diveColliderPos.x,
                _colliderDiveShift,
                _diveColliderPos.z);
        }

        public void SetToDefaultPos()
        {
            var pos = new Vector3(
                transform.position.x,
                _defaultPosY,
                transform.position.z);

            transform.position = pos;
        }

        public Sequence Dive(float changeDuration, float stayDuration)
        {
            var sequence = DOTween.Sequence().Pause();

            sequence
                .Append(transform.DOLocalMoveY(_divePosY[0], changeDuration))
                .Join(_circleSprites[0].DOFade(1, changeDuration))

                .AppendInterval(stayDuration)

                .AppendCallback(() => _collider.center = _diveColliderPos)
                .Append(transform.DOMoveY(_divePosY[1], changeDuration))
                .Join(_circleSprites[1].DOFade(1, changeDuration))

                .AppendInterval(stayDuration)
                .AppendInterval(stayDuration)

                .Append(transform.DOMoveY(_divePosY[0], changeDuration))
                .Join(_circleSprites[1].DOFade(0, changeDuration))
                .AppendCallback(() => _collider.center = _defaultColliderPos)

                .AppendInterval(stayDuration)

                .Append(transform.DOMoveY(_defaultPosY, changeDuration))
                .Join(_circleSprites[0].DOFade(0, changeDuration));

            return sequence;
        }
    }
}