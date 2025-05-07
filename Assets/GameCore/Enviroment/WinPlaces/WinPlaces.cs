using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class WinPlaces : MonoBehaviour,
        IDisposable
    {
        [SerializeField]
        private WinPlace[] _places;

        private readonly List<int> _currentAchieved = new();

        private readonly Dictionary<int,  WinPlace> _placesDict = new();

        public void InitPlaces()
        {
            foreach (var place in _places)
            {
                _placesDict.Add(place.Id, place);

                place.OnAchieved += AddAchievedPlace;
            }
        }

        public bool IsAllWin()
        {
            return _currentAchieved.Count == _places.Length;
        }

        void IDisposable.Dispose()
        {
            foreach (var place in _places)
            {
                place.OnAchieved -= AddAchievedPlace;
            }
        }

        private void AddAchievedPlace(int placeId)
        {
            if (!_placesDict.ContainsKey(placeId))
            {
                Debug.Log($"no winPlace with this id (i.e. xPos) {placeId} in {this.name}!");
                return;
            }

            _currentAchieved.Add(placeId);
        }


    }
}