using UnityEngine;

namespace States
{
    public class PlayerBuildingSingleStructureState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        private string m_StructureName;
        public PlayerBuildingSingleStructureState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
        }
        public override void OnConfirmAction()
        {
            base.OnConfirmAction();
            m_BuildingManager.ConfirmModification();
        }
        public override void OnInputPointerDown(Vector3 position)
        {

            m_BuildingManager.PrepareStructureForPlacement(position, m_StructureName, StructureType.SingleStructure);
        }

        public override void OnBuildArea(string structureName)
        {
        
            base.OnBuildArea(structureName);
            m_BuildingManager.CancelModification();
        }

        public override void OnBuildRoad(string structureName)
        {
        
            base.OnBuildRoad(structureName);
            m_BuildingManager.CancelModification();
        }

        public override void OnCancle()
        {
            m_BuildingManager.CancelModification();
            gameManager.TransitionToState(gameManager.selectionState, null);
        }

        public override void EnterState(string structureName)
        {
            base.EnterState(structureName);
            m_BuildingManager.PrepareBuildingManager(GetType());
            m_StructureName = structureName;
        }
    }
}
