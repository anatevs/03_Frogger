using System;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "LevelConfig",
        menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public RowData[] RowData => _rowData;

        public WinPlacesGOData[] WinPlaceGOData => _winPlaceGOData;

        [SerializeField]
        private WinPlacesGOData[] _winPlaceGOData;

        [SerializeField]
        private RowData[] _rowData;
    }

    [Serializable]
    public struct WinPlacesGOData
    {
        [field: SerializeField]
        public float ShowPeriod { get; private set; }

        [field: SerializeField]
        public float FirstDelay { get; private set; }

        [field: SerializeField]
        public WinPlaceGOConfig Config { get; private set; }
    }
}