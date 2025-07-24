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

        private RowController[] _rowsControllers;

        private RowData[] _rowData;

        private float _cameraX;

        [Inject]
        private void Construct(CameraBorders cameraBorders)
        {
            _cameraX = cameraBorders.CameraHalfX;
        }

        private void Start()
        {
            _rowData = _levelConfig.WaterRowData;

            _rowsControllers = new RowController[_rowData.Length];

            for (int i = 0; i < _rowData.Length; i++)
            {
                var rowData = _rowData[i];

                rowData.ZPos += transform.position.z;

                for (int j = 0; j < rowData.ItemsData.Length; j++)
                {
                    var itemType = rowData.ItemsData[j].Prefab.GetType();

                    var pool = _poolService.GetPool(itemType);

                    rowData.ItemsData[j].Pool = pool;
                }

                _rowsControllers[i] = new RowController(
                    rowData,
                    _cameraX,
                    transform);
            }
        }

        private void Update()
        {
            foreach (var rowController in _rowsControllers)
            {
                rowController.UpdateRow();
            }
        }
    }
}