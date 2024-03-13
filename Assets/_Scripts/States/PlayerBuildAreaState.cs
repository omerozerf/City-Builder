using Managers;
using UnityEngine;

namespace States
{
    public class PlayerBuildAreaState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        private string m_StructureName;
        
        public PlayerBuildAreaState(GameManager gameManager,
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
            Debug.Log("Area");
            m_BuildingManager.PlaceStructureAt(position);
        }


        public BuildingManager GetBuildingManager()
        {
            return m_BuildingManager;
        }
    }
}