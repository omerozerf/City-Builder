using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemolitionState : PlayerState
{
    private BuildingManager m_BuildingManager;
    public PlayerDemolitionState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.m_BuildingManager = buildingManager;
    }

    public override void OnCancle()
    {
        this.m_BuildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void OnConfirmAction()
    {
        this.m_BuildingManager.ConfirmModification();
        base.OnConfirmAction();
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        this.m_BuildingManager.CancelModification();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.m_BuildingManager.CancelModification();
        base.OnBuildRoad(structureName);
    }

    public override void OnBuildArea(string structureName)
    {
        this.m_BuildingManager.CancelModification();
        base.OnBuildArea(structureName);
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.m_BuildingManager.PrepareStructureForDemolitionAt(position);
    }

    public override void EnterState(string variable)
    {
        base.EnterState(variable);
        this.m_BuildingManager.PrepareBuildingManager(this.GetType());
    }

}
