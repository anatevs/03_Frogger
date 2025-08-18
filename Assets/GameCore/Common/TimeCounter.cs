using Cysharp.Threading.Tasks;
using GameManagement;
using System;
using System.Threading;
using UI;
using UnityEngine;

namespace GameCore
{
    public class TimeCounter :
        IStartGameListener,
        ILevelEndListener,
        IRoundEndListener,
        IDamageListener
    {
        public event Action OnTimeIsUp;

        public int CurrentTick => _currentTick;

        private readonly TimerView _view;

        private readonly PointsCounter _pointsCounter;

        private readonly float _tickDuration = 0.5f;

        private readonly int _maxTicks = 60;

        private int _currentTick;

        private CancellationTokenSource _ctn;

        public TimeCounter(TimerView view,
            PointsCounter pointsCounter)
        {
            _view = view;
            _pointsCounter = pointsCounter;
        }

        public void OnStartGame()
        {
            StartTimer();
        }

        public void OnEndLevel()
        {
            CancelTimer();
            StartTimer();
        }

        public void OnDamage()
        {
            CancelTimer();
            StartTimer();
        }

        public void OnEndRound()
        {
            CancelTimer();
        }

        public void StartTimer()
        {
            _ctn = new CancellationTokenSource();

            SetTick(_maxTicks);

            SetTimer(_ctn.Token).Forget();
        }

        private async UniTaskVoid SetTimer(CancellationToken token)
        {
            while (_currentTick != 0)
            {
                SetTick(_currentTick - 1);

                await UniTask.WaitForSeconds(_tickDuration, cancellationToken: token);
            }

            OnTimeIsUp?.Invoke();
        }

        private void CancelTimer()
        {
            _ctn.Cancel();
        }

        private void SetTick(int tick)
        {
            _currentTick = tick;

            _view.SetValue(tick);

            _pointsCounter.SetTicksReward(_currentTick);
        }
    }
}