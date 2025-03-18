using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GameCore
{
    //[RequireComponent(typeof(PlayerInput))]
    public sealed class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _defaultLayer;

        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        private PointerEventData _clickData;

        private List<RaycastResult> _raycastResults;

        private GameControls _controls;

        private GameControls.GameplayActions _gameplayMap;

        private GameControls.UIActions _uiMap;

        private bool _isUIClicked;


        public Vector3 MoveDirection => _moveDirection;

        public Vector3 MovePosition => _movePosition;

        public bool IsMoving => _isMoving;

        public bool IsMovingPos => _isMovingPos;

        private Camera _camera;

        private Plane _groundPlane;

        //a field of input and isPlaying listener to switch input action maps

        //private PlayerInput _playerInput;

        private Vector3 _moveDirection;

        private Vector3 _movePosition;

        private bool _isMoving;

        private bool _isMovingPos;

        private static Vector3 _zeroVector = Vector3.zero;

        private void Awake()
        {
            _controls = new GameControls();

            _gameplayMap = _controls.Gameplay;
            _uiMap = _controls.UI;

            _clickData = new PointerEventData(EventSystem.current);
            _raycastResults = new();

            //playerInput = GetComponent<PlayerInput>();
            _camera = Camera.main;
            _groundPlane = new Plane(Vector3.up, 0);
            _moveDirection = _zeroVector;
        }

        private void OnEnable()
        {
            _controls.Enable();

            _gameplayMap.GameplayClick.started += Click;

            //_playerInput.onActionTriggered += OnMoveUpContinued;
            //_playerInput.onActionTriggered += OnMoveDownContinued;
            //_playerInput.onActionTriggered += OnMoveLeftContinued;
            //_playerInput.onActionTriggered += OnMoveRightContinued;
            //_playerInput.onActionTriggered += OnClickPositionContinued;
        }

        private void OnDisable()
        {
            _gameplayMap.GameplayClick.started -= Click;

            //_playerInput.onActionTriggered -= OnMoveUpContinued;
            //_playerInput.onActionTriggered -= OnMoveDownContinued;
            //_playerInput.onActionTriggered -= OnMoveLeftContinued;
            //_playerInput.onActionTriggered -= OnMoveRightContinued;
            //_playerInput.onActionTriggered -= OnClickPositionContinued;

            _controls.Disable();
        }

        private void Click(InputAction.CallbackContext context)
        {
            var screenPos = context.ReadValue<Vector2>();

            if (!IsUIClicked(screenPos))
            {
                Debug.Log("click go");

                //calc raycast GO position
            }
            else
            {
                Debug.Log("click UI");
            }
        }


        private bool IsUIClicked(Vector2 position)
        {
            _clickData.position = position;

            _raycastResults.Clear();

            _graphicRaycaster.Raycast(_clickData, _raycastResults);

            return _raycastResults.Count > 0;
        }

        private void OnClickPositionContinued(InputAction.CallbackContext context)
        {
            if (context.action.name == "PositionMove")
            {
                if (context.started)
                {
                    var clickPositionScreen = context.ReadValue<Vector2>();

                    var ray = _camera.ScreenPointToRay(clickPositionScreen);

                    if (_groundPlane.Raycast(ray, out var distance))
                    {
                        _isMovingPos = true;
                        _movePosition = ray.GetPoint(distance);
                    }
                }

                else if (context.canceled)
                {
                    _isMovingPos = false;
                    _movePosition = _zeroVector;
                }
            }
        }

        private void OnMoveUpContinued(InputAction.CallbackContext context)
        {
            DirectionMoveContinued("MoveUpValue", Vector3.forward, context);
        }

        private void OnMoveDownContinued(InputAction.CallbackContext context)
        {
            DirectionMoveContinued("MoveDownValue", Vector3.back, context);
        }

        private void OnMoveLeftContinued(InputAction.CallbackContext context)
        {
            DirectionMoveContinued("MoveLeftValue", Vector3.left, context);
        }

        private void OnMoveRightContinued(InputAction.CallbackContext context)
        {
            DirectionMoveContinued("MoveRightValue", Vector3.right, context);
        }

        private void DirectionMoveContinued(string actionName, Vector3 direction, InputAction.CallbackContext context)
        {
            if (context.action.name == actionName)
            {
                if (context.started)
                {
                    _isMoving = true;
                    _moveDirection = direction;
                }
                else if (context.canceled)
                {
                    _isMoving = false;
                    _moveDirection = _zeroVector;
                }
            }
        }

    }
}