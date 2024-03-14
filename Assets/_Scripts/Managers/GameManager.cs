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
        [SerializeField] private StructureRepository _structureRepository;
        
        private BuildingManager m_BuildingManager;
        private PlayerSelectionState m_SelectionState;
        private PlayerBuildingSingleStructureState m_BuildingSingleStructureState;
        private PlayerRemoveBuildingState m_RemoveBuildingState;
        private PlayerBuildZoneState m_BuildZoneState;
        private PlayerBuildingRoadState m_BuildingRoadState;
        private PlayerState m_PlayerState;
        private bool m_IsBuildingMode;
        private int m_CellSize = 2;


        private void Awake()
        {
            _inputManager.SetMouseInputLayerMask(_inputMask);
            _cameraMovement.SetCameraLimits(0, _width, 0, _height);
            
            PrepareStates();

            InitializeListener();
        }
        
        private void OnDestroy()
        {
            DestroyListener();
        }
        

        private void OnLeftMouseDown(Vector3 position)
        { 
            m_PlayerState.OnInputPointerDown(position);
        }
        
        private void InitializeListener()
        {
            _inputManager.OnLeftMouseDown +=
                (position) => m_PlayerState.OnInputPointerDown(position);
            
            _inputManager.OnRightMouseDown +=
                (panPosition) => m_PlayerState.OnInputPanChange(panPosition);
            
            _inputManager.OnRightMouseUp +=
                m_PlayerState.OnInputPanUp;
            
            _inputManager.OnPointerChangeHandler += 
                (position) => m_PlayerState.OnInputPointerChange(position);
            
            
            _uiManager.OnBuildAreaButtonClicked +=
                (structureName) => m_PlayerState.OnBuildArea(structureName);
            
            _uiManager.OnCancelActionButtonClicked +=
                () => m_PlayerState.OnCancel();
            
            _uiManager.OnDemolishButtonClicked += 
                () => m_PlayerState.OnDemolishAction();
            
            _uiManager.OnBuildRoadButtonClicked +=
                (structureName) => m_PlayerState.OnBuildRoad(structureName);
            
            _uiManager.OnBuildSingleStructureButtonClicked +=
                (structureName) => m_PlayerState.OnBuildSingleStructure(structureName);
            
            _uiManager.OnConfirmActionButtonClicked +=
                () => m_PlayerState.OnConfirmAction();
        }

        private void DestroyListener()
        {
            _inputManager.OnLeftMouseDown -= OnLeftMouseDown;
            _inputManager.OnRightMouseDown -= m_PlayerState.OnInputPanChange;
            _inputManager.OnRightMouseUp -= m_PlayerState.OnInputPanUp;
            _inputManager.OnPointerChangeHandler -= m_PlayerState.OnInputPointerChange;
            
            _uiManager.OnBuildAreaButtonClicked -= m_PlayerState.OnBuildArea;
            _uiManager.OnCancelActionButtonClicked -= m_PlayerState.OnCancel;
            _uiManager.OnDemolishButtonClicked -= m_PlayerState.OnDemolishAction;
            _uiManager.OnBuildSingleStructureButtonClicked -= m_PlayerState.OnBuildSingleStructure;
            _uiManager.OnBuildRoadButtonClicked -= m_PlayerState.OnBuildRoad;
            _uiManager.OnConfirmActionButtonClicked -= m_PlayerState.OnConfirmAction;
        }
        
        private void PrepareStates()
        {
            m_BuildingManager = 
                new BuildingManager(_placementManager, m_CellSize, _width, _height, _structureRepository);
            
            m_SelectionState =
                new PlayerSelectionState(this);
            
            m_BuildingSingleStructureState =
                new PlayerBuildingSingleStructureState(this, m_BuildingManager);
            
            m_RemoveBuildingState = 
                new PlayerRemoveBuildingState(this, m_BuildingManager);
            
            m_BuildZoneState =
                new PlayerBuildZoneState(this, m_BuildingManager);
            
            m_BuildingRoadState = 
                new PlayerBuildingRoadState(this, m_BuildingManager);

            m_PlayerState = m_SelectionState;
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
        
        public PlayerBuildZoneState GetBuildAreaState()
        {
            return m_BuildZoneState;
        }
        
        public PlayerBuildingRoadState GetBuildingRoadState()
        {
            return m_BuildingRoadState;
        }

        public CameraMovement GetCameraMovement()
        {
            return _cameraMovement;
        }
    }
}