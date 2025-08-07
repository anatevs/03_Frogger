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
        public readonly float AppearPeriod => _appearPeriod;

        public WinPlaceGOConfig Config;

        [SerializeField]
        private float _showPeriod;

        private float _appearPeriod;

        public void SetupPeriodDuration()
        {
            _appearPeriod = _showPeriod + Config.ActiveDuration;
        }
    }
}