using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace GameCore
{
    public sealed class TurtlesDiver : MovingItem
    {
        [SerializeField]
        private float _changeDuration;

        [SerializeField]
        private float _stayDuration;

        [SerializeField]
        private float _divePeriod;

        [SerializeField]
        private TurtleDiverView[] _views;

        private readonly float _lookRot = 180;

        private CancellationTokenSource _cts = new CancellationTokenSource();


        private void OnDisable()
        {
            _cts.Cancel();
        }

        public override void Init(float speed, (float x, float z) startPos, float lengthScale)
        {
            base.Init(speed, startPos, lengthScale);

            _cts = new CancellationTokenSource();

            for (int i = 0; i < _views.Length; i++)
            {
                MakeDiving(_views[i], _cts.Token).Forget();
            }
        }

        protected override void SetLookDirection()
        {
            var rotPoint = transform.position + Vector3.right * _boxCollider.center.x;

            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].transform.RotateAround(rotPoint, Vector3.up, _lookRot);
            }

            _lookDirection = _moveDirectionX;
        }

        private async UniTaskVoid MakeDiving(TurtleDiverView view, CancellationToken token)
        {
            while (!_cts.IsCancellationRequested)
            {
                await view.Dive(_changeDuration, _stayDuration)
                    .Play()
                    .WithCancellation(token);

                await UniTask.WaitForSeconds(_divePeriod, cancellationToken: token);
            }
        }
    }
}