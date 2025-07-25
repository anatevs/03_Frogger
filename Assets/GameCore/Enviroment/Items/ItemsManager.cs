using GameManagement;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class ItemsManager : MonoBehaviour
    {
        [SerializeField]
        private LevelConfig _levelConfig;

        [SerializeField]
        private PoolsService _poolService;

        [SerializeField]
        private Transform _waterItemsTransform;

        [SerializeField]
        private Transform _roadItemsTransform;

        private float _cameraX;

        private GameListenersManager _listenersManager;

        [Inject]
        private void Construct(CameraBorders cameraBorders,
            GameListenersManager listenersManager)
        {
            _cameraX = cameraBorders.CameraHalfX;

            _listenersManager = listenersManager;
        }

        private void Start()
        {
            InitItems(_levelConfig.WaterRowData, _waterItemsTransform);
        }

        private void InitItems(RowData[] rowsData, Transform itemsTransform)
        {
            for (int i = 0; i < rowsData.Length; i++)
            {
                var rowData = rowsData[i];

                rowData.ZPos += itemsTransform.position.z;

                for (int j = 0; j < rowData.ItemsData.Length; j++)
                {
                    var itemType = rowData.ItemsData[j].Prefab.GetType();

                    var pool = _poolService.GetPool(itemType);

                    rowData.ItemsData[j].Pool = pool;
                }

                var rowController = new RowController(
                    rowData,
                    _cameraX,
                    itemsTransform);

                _listenersManager.AddListener(rowController);
            }
        }
    }
}