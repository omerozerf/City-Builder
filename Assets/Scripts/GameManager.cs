using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using States;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("placementManagerGameObject")] public GameObject _placementManagerGameObject;
    private IPlacementManager m_PlacementManager;
    [FormerlySerializedAs("structureRepository")] public StructureRepository _structureRepository;
    public IInputManager inputManager;
    [FormerlySerializedAs("uiController")] public UiController _uiController;
    [FormerlySerializedAs("width")] public int _width;
    [FormerlySerializedAs("length")] public int _length;
    [FormerlySerializedAs("cameraMovement")] public CameraMovement _cameraMovement;
    [FormerlySerializedAs("inputMask")] public LayerMask _inputMask;
    private BuildingManager m_BuildingManager;
    private int m_CellSize = 3;

    private PlayerState m_State;

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerDemolitionState demolishState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerBuildingZoneState buildingAreaState;
    
    [SerializeField] private ResourceManager _resourceManager;

    public PlayerState State { get => m_State; }

    private void Awake()
    {
        
#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }

    private void PrepareStates()
    {
        m_BuildingManager = new BuildingManager(m_CellSize, _width, _length, m_PlacementManager, _structureRepository, _resourceManager);
        selectionState = new PlayerSelectionState(this);
        demolishState = new PlayerDemolitionState(this, m_BuildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, m_BuildingManager);
        buildingAreaState = new PlayerBuildingZoneState(this, m_BuildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, m_BuildingManager);
        m_State = selectionState;
        m_State.EnterState(null);
    }

    void Start()
    {
        m_PlacementManager = _placementManagerGameObject.GetComponent<IPlacementManager>();
        PrepareStates();
        PreapreGameComponents();
        AssignInputListeners();
        AssignUiControllerListeners();
    }

    private void PreapreGameComponents()
    {
        inputManager.MouseInputMask = _inputMask;
        _cameraMovement.SetCameraLimits(0, _width, 0, _length);
    }

    private void AssignUiControllerListeners()
    {
        _uiController.AddListenerOnBuildAreaEvent((structureName) => m_State.OnBuildArea(structureName));
        _uiController.AddListenerOnBuildSingleStructureEvent((structureName) => m_State.OnBuildSingleStructure(structureName));
        _uiController.AddListenerOnBuildRoadEvent((structureName) => m_State.OnBuildRoad(structureName));
        _uiController.AddListenerOnCancleActionEvent(() => m_State.OnCancle());
        _uiController.AddListenerOnDemolishActionEvent(() => m_State.OnDemolishAction());
        _uiController.AddListenerOnConfirmActionEvent(() => m_State.OnConfirmAction());

    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerDownEvent((position) => m_State.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondDownEvent((position) => m_State.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(() => m_State.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position) => m_State.OnInputPointerChange(position));
        inputManager.AddListenerOnPointerUpEvent(() => m_State.OnInputPointerUp());
    }


    private void HandlePointerChange(Vector3 position)
    {
        m_State.OnInputPointerChange(position);
    }

    private void HandleInputCameraStop()
    {
        m_State.OnInputPanUp();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        m_State.OnInputPanChange(position);
    }

    private void HandleInput(Vector3 position)
    {
        m_State.OnInputPointerDown(position);
    }

    private void StartPlacementMode(string variable)
    {
        TransitionToState(buildingSingleStructureState, variable);
    }

    public void TransitionToState(PlayerState newState, string variable)
    {
        m_State = newState;
        m_State.EnterState(variable);
    }
}
