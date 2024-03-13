using Managers;

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
        
        public override void OnBuildArea(string structureName)
        {
            base.OnBuildArea(structureName);
            
            GetManager().TransitionToState(GetManager().GetBuildAreaState(), structureName);
        }

        public override void OnBuildSingleStructure(string structureName)
        {
            base.OnBuildSingleStructure(structureName);
            
            GetManager().TransitionToState(GetManager().GetBuildingSingleStructureState(), structureName);
        }

        public override void OnBuildRoad(string structureName)
        {
            base.OnBuildRoad(structureName);
            
            GetManager().TransitionToState(GetManager().GetBuildingRoadState(), structureName);
        }

        public override void OnDemolishAction()
        {
            base.OnDemolishAction();
            
            GetManager().TransitionToState(GetManager().GetRemoveBuildingState(), null);
        }


        public BuildingManager GetBuildingManager()
        {
            return m_BuildingManager;
        }
    }
}