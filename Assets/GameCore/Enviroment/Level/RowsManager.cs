using GameManagement;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class RowsManager : MonoBehaviour
    {
        [SerializeField]
        private PoolsService _poolService;

        [SerializeField]
        private Transform _itemsTransform;

        private readonly Dictionary<int, RowController> _rows = new();

        private float _bordersX;

        private GameListenersManager _listenersManager;

        [Inject]
        private void Construct(IBorders borders,
            GameListenersManager listenersManager)
        {
            _bordersX = borders.BordersHalfX;

            _listenersManager = listenersManager;
        }

        public void InitLevelRows(RowData[] rowData)
        {
            InitZoneRows(rowData, _itemsTransform, _rows);
        }

        public void SetupLevelRows(RowData[] rowData)
        {
            SetupZoneRows(rowData, _rows);
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
                    _bordersX,
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
            for (int i = 0; i < rowData.ItemsData.Length; i++)
            {
                //var itemType = rowData.ItemsData[i].Prefab.GetType();

                //var pool = _poolService.GetPool(itemType);


                var itemId = rowData.ItemsData[i].Prefab.Id;

                var pool = _poolService.GetPool(itemId);


                rowData.ItemsData[i].Pool = pool;
            }
        }
    }
}