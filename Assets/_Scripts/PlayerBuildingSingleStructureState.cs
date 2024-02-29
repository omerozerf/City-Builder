using UnityEngine;

namespace _Scripts
{
    public class PlayerBuildingSingleStructureState : PlayerState
    {
        private GridStructure m_GridStructure;
        private PlacementManager m_PlacementManager;
        
        
        public PlayerBuildingSingleStructureState(GameManager gameManager, GridStructure gridStructure,
            PlacementManager placementManager) : base(gameManager)
        {
            m_GridStructure = gridStructure;
            m_PlacementManager = placementManager;
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);

            if (!m_GridStructure.IsCellTaken(gridPosition))
            {
                m_PlacementManager.Build(gridPosition, m_GridStructure);
            }
            
        }

        public override void OnInputPointerChange(Vector3 position)
        {
            return;
        }

        public override void OnInputPointerUp()
        {
            return;
        }

        public override void OnInputPanChange(Vector3 panPosition)
        {
            return;
        }

        public override void OnInputPanUp()
        {
            return;
        }

        protected override void OnCancel()
        {
            return;
        }
    }
}