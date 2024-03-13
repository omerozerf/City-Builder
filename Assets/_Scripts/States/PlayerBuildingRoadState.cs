using Managers;

namespace States
{
    public class PlayerBuildingRoadState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        
        public PlayerBuildingRoadState(GameManager gameManager,
            BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
        }

        public override void OnCancel()
        {
            
        }
        
        public BuildingManager GetBuildingManager()
        {
            return m_BuildingManager;
        }
    }
}