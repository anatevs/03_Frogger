using UnityEngine;
using VContainer;

namespace GameCore
{
    public class ItemsManagerT : MonoBehaviour
    {
        [SerializeField]
        private RowData[] _rowData;

        [SerializeField]
        private TPoolsService _poolService;

        private RowControllerT[] _rowsControllers;

        private float[] _zPositions;

        private float _cameraX;

        [Inject]
        private void Construct(CameraBorders cameraBorders)
        {
            _cameraX = cameraBorders.CameraHalfX;
        }

        private void Start()
        {
            _zPositions = new float[_rowData.Length];

            _rowsControllers = new RowControllerT[_rowData.Length];


            for (int i = 0; i < _rowData.Length; i++)
            {
                _zPositions[i] = _rowData[i].ZPos + transform.position.z;

                for (int j = 0; j < _rowData[i].ItemsData.Length; j++)
                {
                    //Debug.Log($"{i}, {j}");

                    var itemType = _rowData[i].ItemsData[j].Prefab.GetType();

                    var pool = _poolService.GetPool(itemType);

                    _rowData[i].ItemsData[j].Pool = pool;
                }

                _rowsControllers[i] = new RowControllerT(
                    _rowData[i],
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