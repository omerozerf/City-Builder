using System;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private PlacementManager _placementManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private CameraMovement _cameraMovement;
        
        private GridStructure m_GridStructure;
        private PlayerSelectionState m_SelectionState;
        private PlayerBuildingSingleStructureState m_BuildingSingleStructureState;
        private PlayerState m_PlayerState;
        private bool m_IsBuildingMode;
        private int m_CellSize = 2;


        private void Awake()
        {
            m_GridStructure = new GridStructure(m_CellSize, _width, _height);
            _cameraMovement.SetCameraLimits(0, _width, 0, _height);
            
            m_SelectionState =
                new PlayerSelectionState(this, _cameraMovement);
            m_BuildingSingleStructureState =
                new PlayerBuildingSingleStructureState(this, m_GridStructure, _placementManager);

            m_PlayerState = m_SelectionState;
            
            InitializeListener();
        }
        
        private void OnDestroy()
        {
            DestroyListener();
        }


        private void OnPointerChangeHandler(Vector3 pos)
        {
            m_PlayerState.OnInputPointerChange(pos);
        }
        
        private void OnLeftMouseUp()
        {
            Debug.Log("Left mouse up");
        }
        
        private void OnRightMouseUp()
        {
            m_PlayerState.OnInputPanUp();
        }
        
        private void OnRightMouseDown(Vector3 pos)
        {
            m_PlayerState.OnInputPanChange(pos);
        }
        
        private void OnBuildResidentialAreaButtonClicked()
        {
            TransitionToState(m_BuildingSingleStructureState);
        }
        
        private void OnCancelActionButtonClicked()
        {
            m_PlayerState.OnCancel();
        }

        private void OnLeftMouseDown(Vector3 position)
        { 
            m_PlayerState.OnInputPointerDown(position);
        }
        
        private void InitializeListener()
        {
            _inputManager.OnLeftMouseDown += OnLeftMouseDown;
            _inputManager.OnRightMouseDown += OnRightMouseDown;
            _inputManager.OnRightMouseUp += OnRightMouseUp;
            _inputManager.OnLeftMouseUp += OnLeftMouseUp;
            _inputManager.OnPointerChangeHandler += OnPointerChangeHandler;
            
            _uiManager.OnBuildResidentialAreaButtonClicked += OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked += OnCancelActionButtonClicked;
        }
        
        private void DestroyListener()
        {
            _inputManager.OnLeftMouseDown -= OnLeftMouseDown;
            _inputManager.OnRightMouseDown -= OnRightMouseDown;
            _inputManager.OnRightMouseUp -= OnRightMouseUp;
            _inputManager.OnLeftMouseUp -= OnLeftMouseUp;
            _inputManager.OnPointerChangeHandler -= OnPointerChangeHandler;
            
            _uiManager.OnBuildResidentialAreaButtonClicked -= OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked -= OnCancelActionButtonClicked;
        }
        
        public void TransitionToState(PlayerState state)
        {
            m_PlayerState = state;
            m_PlayerState.EnterState();
        }
        
        public PlayerSelectionState GetSelectionState()
        {
            return m_SelectionState;
        }
        
        public PlayerBuildingSingleStructureState GetBuildingSingleStructureState()
        {
            return m_BuildingSingleStructureState;
        }
    }
}
