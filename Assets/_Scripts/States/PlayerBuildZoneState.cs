using Managers;
using Types;
using UnityEngine;

namespace States
{
    public class PlayerBuildZoneState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        private string m_StructureName;
        
        public PlayerBuildZoneState(GameManager gameManager,
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
        
        public override void OnInputPointerDown(Vector3 position)
        {
            m_BuildingManager.PlaceStructureAt(position, m_StructureName, StructureType.Zone);
        }


        public BuildingManager GetBuildingManager()
        {
            return m_BuildingManager;
        }
    }
}