using UnityEngine;

namespace States
{
    public class PlayerDemolitionState : PlayerState
    {
        private BuildingManager m_BuildingManager;
        public PlayerDemolitionState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
        {
            m_BuildingManager = buildingManager;
        }

        public override void OnCancle()
        {
            m_BuildingManager.CancelModification();
            gameManager.TransitionToState(gameManager.selectionState, null);
        }

        public override void OnConfirmAction()
        {
            m_BuildingManager.ConfirmModification();
            base.OnConfirmAction();
        }

        public override void OnBuildSingleStructure(string structureName)
        {
            m_BuildingManager.CancelModification();
            base.OnBuildSingleStructure(structureName);
        }

        public override void OnBuildRoad(string structureName)
        {
            m_BuildingManager.CancelModification();
            base.OnBuildRoad(structureName);
        }

        public override void OnBuildArea(string structureName)
        {
            m_BuildingManager.CancelModification();
            base.OnBuildArea(structureName);
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            m_BuildingManager.PrepareStructureForDemolitionAt(position);
        }

        public override void EnterState(string variable)
        {
            base.EnterState(variable);
            m_BuildingManager.PrepareBuildingManager(GetType());
        }

    }
}
