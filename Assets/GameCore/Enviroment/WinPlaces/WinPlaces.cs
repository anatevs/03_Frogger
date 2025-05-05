using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class WinPlaces : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _places;

        private readonly List<int> _currentAchieved = new();

        private readonly Dictionary<int, Transform> _placesDict = new();

        public void InitPlaces()
        {
            foreach (var place in _places)
            {
                _placesDict.Add(GetPlaceId(place), place);
            }
        }

        public void AddAchievedPlace(Transform place)
        {
            var id = GetPlaceId(place);

            if (!_placesDict.ContainsKey(id))
            {
                Debug.Log($"no winPlace with this id (i.e. xPos) {id} in {this.name}!");
                return;
            }

            _currentAchieved.Add(id);
        }

        public bool IsAllWin()
        {
            return _currentAchieved.Count == _places.Length;
        }

        private int GetPlaceId(Transform place)
        {
            return (int)place.position.x;
        }
    }
}