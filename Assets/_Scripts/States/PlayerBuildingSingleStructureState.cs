using Managers;
using Types;
using UnityEngine;

namespace States
{
    public class PlayerBuildingSingleStructureState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        private string m_StructureName;
        
        public PlayerBuildingSingleStructureState(GameManager gameManager,
            BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            Debug.Log("Single Structure");
            
            m_BuildingManager.PlaceStructureAt(position, m_StructureName, StructureType.Facility);
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
            GetManager().TransitionToState(GetManager().GetSelectionState(), null);
        }

        public override void EnterState(string structureName)
        {
            base.EnterState(structureName);
            
            m_StructureName = structureName;
        }
    }
}