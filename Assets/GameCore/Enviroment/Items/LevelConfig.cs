using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "LevelConfig",
        menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public RowData[] RowData => _rowData;

        [SerializeField]
        private RowData[] _rowData;
    }
}