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

        private readonly GameListenersManager _listenersManager;

        private readonly List<int> _currentAchieved = new();

        private readonly List<int> _currentFree = new();

        private WinPlacesGOData[] _goData;

        private GameObject[] _accidentalGO;

        private CancellationTokenSource _ctn;

        public WinPlaces(WinPlace[] places,
            GameListenersManager listenersManager)
        {
            _places = places;
            _listenersManager = listenersManager;
        }

        void IInitializable.Initialize()
        {
            foreach (var place in _places)
            {
                place.OnAchieved += AddAchievedPlace;
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
            for (int i = 0; i < _places.Length; i++)
            {
                _currentFree.Add(i);
            }

            if (_accidentalGO != null)
            {
                foreach (var go in _accidentalGO)
                {
                    GameObject.Destroy(go);
                }
            }

            _ctn = new CancellationTokenSource();

            _goData = goData;

            _accidentalGO = new GameObject[_goData.Length];

            for (int i = 0; i < _accidentalGO.Length; i++)
            {
                var go = GameObject.Instantiate(_goData[i].Config.Prefab,
                    _places[0].transform.parent);

                _accidentalGO[i] = go;

                ShowAccidentalGO(i, _ctn.Token).Forget();
            }
        }

        private async UniTaskVoid ShowAccidentalGO(int goID, CancellationToken token)
        {
            for (int i = 0; i <= 3; i++)
            {
                var randomIndex = UnityEngine.Random.Range(0, _currentFree.Count);

                var placeId = _currentFree[randomIndex];

                ShowGO(placeId, goID);

                await UniTask.WaitForSeconds(_goData[goID].AppearPeriod,
                    cancellationToken: token);
            }


            //while (!IsAllWin())
            //{

            //}
        }

        private void AddAchievedPlace(int placeId)
        {
            _currentAchieved.Add(placeId);
            _currentFree.Remove(placeId);

            _listenersManager.EndRound();

            if (IsAllWin())
            {
                _listenersManager.EndLevel();

                _currentAchieved.Clear();
                _currentFree.Clear();

                for (int i = 0; i < _places.Length; i++)
                {
                    _places[i].SetAchieved(false);

                    _currentFree.Add(i);
                }

                foreach (var go in _accidentalGO)
                {
                    GameObject.Destroy(go);
                }

                _ctn.Cancel();

                return;
            }
        }
        private bool IsAllWin()
        {
            return _currentAchieved.Count == 1;// _places.Length;
        }

        private void ShowGO(int placeId, int goId)
        {
            var (sequence, isEnemy) = _goData[goId].Config
                .Show(_accidentalGO[goId].transform,
                _places[placeId].transform.position.x);

            _places[placeId].PlaceGO(sequence, isEnemy);
        }
    }
}