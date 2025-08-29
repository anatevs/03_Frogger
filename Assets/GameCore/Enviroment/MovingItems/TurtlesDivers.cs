using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace GameCore
{
    public sealed class TurtlesDivers : MovingItem
    {
        [SerializeField]
        private TurtleDiverView[] _views;

        private float _changeDuration = 0.5f;

        private float _stayDuration = 0.6f;

        private float _divePeriod = 3f;

        private float[] _startDelay = { 0f, 2f };

        private readonly float _lookRot = 180;

        private CancellationTokenSource _cts = new();

        private void OnDisable()
        {
            _cts.Cancel();
        }

        public override void Init(float speed, (float x, float z) startPos, float lengthScale)
        {
            base.Init(speed, startPos, lengthScale);

            _cts = new CancellationTokenSource();

            UniTask
                .WaitForSeconds(Random.Range(_startDelay[0], _startDelay[1]),
                cancellationToken: _cts.Token)
                .Forget();


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