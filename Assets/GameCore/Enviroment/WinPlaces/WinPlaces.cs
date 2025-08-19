using Cysharp.Threading.Tasks;
using GameManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public class WinPlaces :
        IInitializable,
        IDisposable
    {
        private readonly WinPlace[] _places;

        private readonly Dictionary<int, WinPlace> _winPlaces = new();

        private readonly GameListenersManager _listenersManager;

        private readonly PlayerJump _playerJump;

        private readonly PointsCounter _playersCounter;

        private readonly List<int> _currentAchieved = new();

        private readonly List<int> _currentFree = new();

        private WinPlacesGOData[] _goData;

        private GameObject[] _accidentalGO;

        private CancellationTokenSource _ctn;

        public WinPlaces(WinPlace[] places,
            GameListenersManager listenersManager,
            PlayerJump playerJump,
            PointsCounter playersCounter)
        {
            _places = places;
            _listenersManager = listenersManager;
            _playerJump = playerJump;
            _playersCounter = playersCounter;
        }

        void IInitializable.Initialize()
        {
            for (int i = 0; i < _places.Length; i++)
            {
                _places[i].OnAchieved += AddAchievedPlace;

                _places[i].Init(_playerJump);

                _winPlaces.Add(_places[i].Id, _places[i]);

                _currentFree.Add(_places[i].Id);
            }
        }

        void IDisposable.Dispose()
        {
            foreach (var place in _places)
            {
                place.OnAchieved -= AddAchievedPlace;
            }
        }

        public void SetupWinPlaces(WinPlacesGOData[] goData)
        {
            _ctn = new CancellationTokenSource();

            _goData = goData;

            _accidentalGO = new GameObject[_goData.Length];

            for (int i = 0; i < _accidentalGO.Length; i++)
            {
                var go = GameObject.Instantiate(_goData[i].Config.Prefab,
                    _places[0].transform.parent);

                if (go.TryGetComponent<Fly>(out var fly))
                {
                    fly.Init(_playerJump.gameObject, _playersCounter);
                }

                go.SetActive(false);

                _accidentalGO[i] = go;

                ShowAccidentalGO(i, _ctn.Token).Forget();
            }
        }

        private async UniTaskVoid ShowAccidentalGO(int goId, CancellationToken token)
        {
            var appearPeriod = _goData[goId].ShowPeriod;

            await UniTask.WaitForSeconds(_goData[goId].FirstDelay);

            while (!IsAllWin())
            {
                var randomIndex = UnityEngine.Random.Range(0, _currentFree.Count);

                var placeId = _currentFree[randomIndex];

                _accidentalGO[goId].SetActive(true);

                await ShowGO(placeId, goId, token);

                if (!token.IsCancellationRequested)
                {
                    _accidentalGO[goId].SetActive(false);
                }

                await UniTask.WaitForSeconds(appearPeriod,
                    cancellationToken: token);
            }
        }

        private void AddAchievedPlace(int placeId)
        {
            _currentAchieved.Add(placeId);
            _currentFree.Remove(placeId);

            _listenersManager.EndRound();

            if (IsAllWin())
            {
                _ctn.Cancel();

                foreach (var go in _accidentalGO)
                {
                    GameObject.Destroy(go);
                }

                _listenersManager.EndLevel();

                _currentAchieved.Clear();
                _currentFree.Clear();

                for (int i = 0; i < _places.Length; i++)
                {
                    _places[i].SetAchieved(false);

                    _currentFree.Add(i);
                }

                return;
            }
        }
        private bool IsAllWin()
        {
            return _currentAchieved.Count == 2;// _places.Length;
        }

        private UniTask ShowGO(int placeId, int goId, CancellationToken token)
        {
            var place = _winPlaces[placeId];

            var (sequence, isEnemy) = _goData[goId].Config
                .Show(_accidentalGO[goId].transform,
                place.transform.position.x);

            return place.PlaceGO(sequence, isEnemy, token);
        }
    }
}