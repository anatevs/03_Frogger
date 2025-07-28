using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "LevelConfig",
        menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public RowData[] WaterRowData => _waterRowData;

        public RowData[] RoadRowData => _roadRowData;

        [SerializeField]
        private RowData[] _waterRowData;

        [SerializeField]
        private RowData[] _roadRowData;
    }
}