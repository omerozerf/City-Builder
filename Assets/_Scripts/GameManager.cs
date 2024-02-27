using System;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlacementManager _placementManager;
        [SerializeField] private InputManager _inputManager;
        
        private GridStructure m_GridStructure;


        private void Awake()
        {
            m_GridStructure = new GridStructure(2);
            
            _inputManager.OnInputCalculated += OnInputCalculated;
        }

        private void OnDestroy()
        {
            _inputManager.OnInputCalculated -= OnInputCalculated;
        }


        private void OnInputCalculated(Vector3 position)
        {
            Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);
            _placementManager.Build(gridPosition);
        }
    }
}
