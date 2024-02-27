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
            
            _inputManager.OnInputCalculated += OnInputCalculated;
            _inputManager.OnPointerDownHandler += OnPointerDownHandler;
            _inputManager.OnPointerUpHandler += OnPointerUpHandler;
            _uiManager.OnBuildResidentialAreaButtonClicked += OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked += OnCancelActionButtonClicked;
        }

        private void OnDestroy()
        {
            _inputManager.OnInputCalculated -= OnInputCalculated;
            _inputManager.OnPointerDownHandler -= OnPointerDownHandler;
            _inputManager.OnPointerUpHandler += OnPointerUpHandler;
            _uiManager.OnBuildResidentialAreaButtonClicked -= OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked -= OnCancelActionButtonClicked;
        }
        
        
        private void OnPointerUpHandler()
        {
            _cameraMovement.StopCameraMovement();
        }
        
        private void OnPointerDownHandler(Vector3 pos)
        {
            if (!m_IsBuildingMode)
            {
                _cameraMovement.MoveCamera(pos);
            }
        }
        
        private void OnBuildResidentialAreaButtonClicked()
        {
            m_IsBuildingMode = true;
        }
        
        private void OnCancelActionButtonClicked()
        {
            m_IsBuildingMode = false;
        }

        private void OnInputCalculated(Vector3 position)
        { 
            Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);

            if (m_IsBuildingMode && !m_GridStructure.IsCellTaken(gridPosition))
            {
                _placementManager.Build(gridPosition, m_GridStructure);
            }
        }
    }
}
