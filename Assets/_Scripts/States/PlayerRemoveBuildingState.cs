using Managers;
using UnityEngine;

namespace States
{
    public class PlayerRemoveBuildingState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        
        
        public PlayerRemoveBuildingState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            m_BuildingManager.RemoveBuildingAt(position);
        }

        public override void OnInputPointerChange(Vector3 position)
        {
            return;
        }

        public override void OnInputPointerUp()
        {
            return;
        }
        

        public override void OnCancel()
        {
            GetManager().TransitionToState(GetManager().GetSelectionState(), null);
        }
    }
}