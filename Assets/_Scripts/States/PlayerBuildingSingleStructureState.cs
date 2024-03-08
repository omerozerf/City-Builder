using Managers;
using UnityEngine;

namespace States
{
    public class PlayerBuildingSingleStructureState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        
        
        public PlayerBuildingSingleStructureState(GameManager gameManager,
            BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            m_BuildingManager.PlaceStructureAt(position);
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


        public override void OnCancel()
        {
            GetManager().TransitionToState(GetManager().GetSelectionState());
        }
    }
}