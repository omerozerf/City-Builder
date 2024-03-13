using States;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private PlacementManager _placementManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private LayerMask _inputMask;
        
        private BuildingManager m_BuildingManager;
        private PlayerSelectionState m_SelectionState;
        private PlayerBuildingSingleStructureState m_BuildingSingleStructureState;
        private PlayerRemoveBuildingState m_RemoveBuildingState;
        private PlayerState m_PlayerState;
        private bool m_IsBuildingMode;
        private int m_CellSize = 2;


        private void Awake()
        {
            _inputManager.SetMouseInputLayerMask(_inputMask);
            _cameraMovement.SetCameraLimits(0, _width, 0, _height);
            
            m_BuildingManager = 
                new BuildingManager(_placementManager, m_CellSize, _width, _height);
            m_SelectionState =
                new PlayerSelectionState(this, _cameraMovement);
            m_BuildingSingleStructureState =
                new PlayerBuildingSingleStructureState(this, m_BuildingManager);
            m_RemoveBuildingState = 
                new PlayerRemoveBuildingState(this, m_BuildingManager);

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
        
        private void OnBuildAreaButtonClicked(string variable)
        {
            TransitionToState(m_BuildingSingleStructureState, variable);
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
            
            _uiManager.OnBuildAreaButtonClicked += OnBuildAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked += OnCancelActionButtonClicked;
            _uiManager.OnDemolishButtonClicked += OnDemolishButtonClicked;
        }

        private void OnDemolishButtonClicked()
        {
            TransitionToState(m_RemoveBuildingState, null);
        }

        private void DestroyListener()
        {
            _inputManager.OnLeftMouseDown -= OnLeftMouseDown;
            _inputManager.OnRightMouseDown -= OnRightMouseDown;
            _inputManager.OnRightMouseUp -= OnRightMouseUp;
            _inputManager.OnLeftMouseUp -= OnLeftMouseUp;
            _inputManager.OnPointerChangeHandler -= OnPointerChangeHandler;
            
            _uiManager.OnBuildAreaButtonClicked -= OnBuildAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked -= OnCancelActionButtonClicked;
            _uiManager.OnDemolishButtonClicked -= OnDemolishButtonClicked;
        }
        
    
        public void TransitionToState(PlayerState state, string variable)
        {
            m_PlayerState = state;
            m_PlayerState.EnterState(variable);
        }
        
        public PlayerSelectionState GetSelectionState()
        {
            return m_SelectionState;
        }
        
        public PlayerBuildingSingleStructureState GetBuildingSingleStructureState()
        {
            return m_BuildingSingleStructureState;
        }
        
        public PlayerRemoveBuildingState GetRemoveBuildingState()
        {
            return m_RemoveBuildingState;
        }
    }
}