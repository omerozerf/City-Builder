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
        private bool m_IsBuildingMode;


        private void Awake()
        {
            m_GridStructure = new GridStructure(2, _width, _height);
            _cameraMovement.SetCameraLimits(0, _width, 0, _height);
            
            _inputManager.OnLeftMouseDown += OnLeftMouseDown;
            _inputManager.OnRightMouseDown += OnRightMouseDown;
            _inputManager.OnRightMouseUp += OnRightMouseUp;
            _inputManager.OnLeftMouseUp += OnLeftMouseUp;
            _inputManager.OnPointerChangeHandler += OnPointerChangeHandler;
            
            _uiManager.OnBuildResidentialAreaButtonClicked += OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked += OnCancelActionButtonClicked;
        }
        
        private void OnDestroy()
        {
            _inputManager.OnLeftMouseDown -= OnLeftMouseDown;
            _inputManager.OnRightMouseDown -= OnRightMouseDown;
            _inputManager.OnRightMouseUp -= OnRightMouseUp;
            _inputManager.OnLeftMouseUp -= OnLeftMouseUp;
            _inputManager.OnPointerChangeHandler -= OnPointerChangeHandler;
            
            _uiManager.OnBuildResidentialAreaButtonClicked -= OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked -= OnCancelActionButtonClicked;
        }
        
        
        private void OnPointerChangeHandler(Vector3 obj)
        {
            Debug.Log(obj);
        }
        
        private void OnLeftMouseUp()
        {
            Debug.Log("Left mouse up");
        }
        
        private void OnRightMouseUp()
        {
            return;
        }
        
        private void OnRightMouseDown(Vector3 pos)
        {
            return;
        }
        
        private void OnBuildResidentialAreaButtonClicked()
        {
            m_IsBuildingMode = true;
        }
        
        private void OnCancelActionButtonClicked()
        {
            m_IsBuildingMode = false;
        }

        private void OnLeftMouseDown(Vector3 position)
        { 
            return;
        }
    }
}
