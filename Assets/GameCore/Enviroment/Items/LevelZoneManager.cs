using GameManagement;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class LevelZoneManager : MonoBehaviour,
        IEndLevelListener
    {
        [SerializeField]
        private LevelConfig[] _levelConfigs;

        [SerializeField]
        private PoolsService _poolService;

        [SerializeField]
        private Transform _waterItemsTransform;

        [SerializeField]
        private Transform _roadItemsTransform;

        private readonly Dictionary<int, RowController> _waterRows = new();

        private readonly Dictionary<int, RowController> _roadRows = new();

        private float _cameraX;

        private GameListenersManager _listenersManager;

        private int _currentLevelIndex;

        [Inject]
        private void Construct(CameraBorders cameraBorders,
            GameListenersManager listenersManager)
        {
            _cameraX = cameraBorders.CameraHalfX;

            _listenersManager = listenersManager;

            _listenersManager.AddListener(this);
        }

        public void OnEndLevel()
        {
            Debug.Log("on end level");

            _currentLevelIndex++;

            if (_currentLevelIndex >= _levelConfigs.Length)
            {
                _currentLevelIndex = 0;

                Debug.Log("All levels completed, restarting from the first level.");

                return;
            }

            SetupLevelRows(_levelConfigs[_currentLevelIndex]);
        }

        private void InitLevelRows(LevelConfig levelConfig)
        {
            InitZoneRows(levelConfig.WaterRowData, _waterItemsTransform, _waterRows);
            //InitItems(levelConfig.RoadRowData, _roadItemsTransform, _roadRows);
        }

        private void SetupLevelRows(LevelConfig levelConfig)
        {
            SetupZoneRows(levelConfig.WaterRowData, _waterRows);
            //SetupItems(levelConfig.RoadRowData, _roadRows);
        }

        private void Start()
        {
            _currentLevelIndex = 0;

            InitLevelRows(_levelConfigs[_currentLevelIndex]);
        }

        private void InitZoneRows(RowData[] rowsData,
            Transform itemsTransform,
            Dictionary<int, RowController> rows)
        {
            foreach (var rowData in rowsData)
            {
                SetupRowPools(rowData);

                var rowController = new RowController(
                    rowData,
                    _cameraX,
                    itemsTransform);

                _listenersManager.AddListener(rowController);

                rows.Add(rowData.ZPos, rowController);
            }
        }

        private void SetupZoneRows(RowData[] rowsData, Dictionary<int, RowController> rows)
        {
            foreach (var rowData in rowsData)
            {
                SetupRowPools(rowData);

                rows[rowData.ZPos]
                    .SetupController(rowData);
            }
        }

        private void SetupRowPools(RowData rowData)
        {
            for (int j = 0; j < rowData.ItemsData.Length; j++)
            {
                var itemType = rowData.ItemsData[j].Prefab.GetType();

                var pool = _poolService.GetPool(itemType);

                rowData.ItemsData[j].Pool = pool;
            }
        }
    }
}