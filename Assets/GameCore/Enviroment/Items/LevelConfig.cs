using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "LevelConfig",
        menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public RowData[] WaterRowData => _waterRowData;

        [SerializeField]
        private RowData[] _waterRowData;
    }
}