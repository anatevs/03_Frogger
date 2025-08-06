using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "LevelConfig",
        menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {


        public RowData[] RowData => _rowData;

        [SerializeField]
        private WinPlaceGOConfig _winPlaceEnemy;

        [SerializeField]
        private float _winPlaceEnemyProb;

        [SerializeField]
        private RowData[] _rowData;
    }
}