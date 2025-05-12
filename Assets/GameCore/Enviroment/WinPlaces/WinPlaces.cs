using GameManagement;
using System;
using System.Collections.Generic;
using VContainer.Unity;
using UnityEngine;

namespace GameCore
{
    public class WinPlaces :
        IInitializable,
        IDisposable
    {
        private readonly WinPlace[] _places;

        private readonly GameListenersManager _listenersManager;

        private readonly List<int> _currentAchieved = new();

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

        public bool IsAllWin()
        {
            return _currentAchieved.Count == _places.Length;
        }

        private void AddAchievedPlace(int placeId)
        {
            _currentAchieved.Add(placeId);

            if (IsAllWin())
            {
                Debug.Log("all achieved");

                _listenersManager.OnEndGame();

                _currentAchieved.Clear();

                return;
            }

            _listenersManager.OnEndRound();
        }
    }
}