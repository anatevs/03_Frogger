using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GameCore
{
    public sealed class InputHandler : MonoBehaviour
    {
        public event Action<Vector3> OnMoved;

        public Vector3 MovePosition => _movePosition;

        public bool IsMovingPos => _isMovingPos;

        [SerializeField]
        private LayerMask _defaultLayer;

        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        private PointerEventData _clickData;

        private List<RaycastResult> _raycastResults;

        private GameControls _controls;

        private GameControls.GameplayActions _gameplayMap;

        private GameControls.UIActions _uiMap;

        private Camera _camera;

        private Plane _groundPlane;

        private Vector3 _moveDirection;

        private Vector3 _movePosition;

        private bool _isMoving;

        private bool _isMovingPos;

        private static Vector3 _zeroVector = Vector3.zero;

        private readonly Dictionary<string, Vector3> _moveVectors = new();


        private void Awake()
        {
            _controls = new GameControls();

            _gameplayMap = _controls.Gameplay;
            //_uiMap = _controls.UI;

            _clickData = new PointerEventData(EventSystem.current);
            _raycastResults = new();

            _camera = Camera.main;
            _groundPlane = new Plane(Vector3.up, 0);
            _moveDirection = _zeroVector;

            _moveVectors.Add(_gameplayMap.MoveUp.name, Vector3.forward);
            _moveVectors.Add(_gameplayMap.MoveDown.name, Vector3.back);
            _moveVectors.Add(_gameplayMap.MoveLeft.name, Vector3.left);
            _moveVectors.Add(_gameplayMap.MoveRight.name, Vector3.right);
        }

        private void OnEnable()
        {
            _controls.Enable();

            _gameplayMap.MoveUp.started += DirectionMoveContinued;
            _gameplayMap.MoveDown.started += DirectionMoveContinued;
            _gameplayMap.MoveLeft.started += DirectionMoveContinued;
            _gameplayMap.MoveRight.started += DirectionMoveContinued;

            _gameplayMap.MoveUp.canceled += DirectionMoveContinued;
            _gameplayMap.MoveDown.canceled += DirectionMoveContinued;
            _gameplayMap.MoveLeft.canceled += DirectionMoveContinued;
            _gameplayMap.MoveRight.canceled += DirectionMoveContinued;
        }

        private void OnDisable()
        {
            _gameplayMap.MoveUp.started -= DirectionMoveContinued;
            _gameplayMap.MoveDown.started -= DirectionMoveContinued;
            _gameplayMap.MoveLeft.started -= DirectionMoveContinued;
            _gameplayMap.MoveRight.started -= DirectionMoveContinued;

            _gameplayMap.MoveUp.canceled -= DirectionMoveContinued;
            _gameplayMap.MoveDown.canceled -= DirectionMoveContinued;
            _gameplayMap.MoveLeft.canceled -= DirectionMoveContinued;
            _gameplayMap.MoveRight.canceled -= DirectionMoveContinued;



            _controls.Disable();
        }

        private void Update()
        {
            if (_isMoving)
            {
                OnMoved?.Invoke(_moveDirection);
            }
        }

        private bool IsUIClicked(Vector2 position)
        {
            _clickData.position = position;

            _raycastResults.Clear();

            _graphicRaycaster.Raycast(_clickData, _raycastResults);

            return _raycastResults.Count > 0;
        }

        private void ClickPositionContinued(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var clickPositionScreen = context.ReadValue<Vector2>();

                if (IsUIClicked(clickPositionScreen))
                {
                    var ray = _camera.ScreenPointToRay(clickPositionScreen);

                    if (_groundPlane.Raycast(ray, out var distance))
                    {
                        _isMovingPos = true;
                        _movePosition = ray.GetPoint(distance);
                    }
                }
            }

            else if (context.canceled)
            {
                _isMovingPos = false;
                _movePosition = _zeroVector;
            }
        }

        private void DirectionMoveContinued(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isMoving = true;
                _moveDirection = _moveVectors[context.action.name];
            }
            else if (context.canceled)
            {
                _isMoving = false;
                _moveDirection = _zeroVector;
            }
        }
    }
}