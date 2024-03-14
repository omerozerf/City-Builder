using Managers;
using Types;
using UnityEngine;

namespace States
{
    public class PlayerBuildingRoadState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        private string m_StructureName;
        
        
        public PlayerBuildingRoadState(GameManager gameManager,
            BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
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
        
        public BuildingManager GetBuildingManager()
        {
            return m_BuildingManager;
        }
        
        public override void OnInputPointerDown(Vector3 position)
        {
            m_BuildingManager.PlaceStructureAt(position, m_StructureName, StructureType.Road);
        }
    }
}