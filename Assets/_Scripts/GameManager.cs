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
        
        private GridStructure m_GridStructure;
        private bool m_BuildingModeActive;


        private void Awake()
        {
            m_GridStructure = new GridStructure(2, _width, _height);
            
            _inputManager.OnInputCalculated += OnInputCalculated;
            _uiManager.OnBuildResidentialAreaButtonClicked += OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked += OnCancelActionButtonClicked;
        }

        private void OnDestroy()
        {
            _inputManager.OnInputCalculated -= OnInputCalculated;
            _uiManager.OnBuildResidentialAreaButtonClicked -= OnBuildResidentialAreaButtonClicked;
            _uiManager.OnCancelActionButtonClicked -= OnCancelActionButtonClicked;
        }
        
        
        private void OnBuildResidentialAreaButtonClicked()
        {
            m_BuildingModeActive = true;
        }
        
        private void OnCancelActionButtonClicked()
        {
            m_BuildingModeActive = false;
        }

        private void OnInputCalculated(Vector3 position)
        {
            Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);

            if (m_BuildingModeActive && !m_GridStructure.IsCellTaken(gridPosition))
            {
                _placementManager.Build(gridPosition, m_GridStructure);
            }
        }
    }
}
