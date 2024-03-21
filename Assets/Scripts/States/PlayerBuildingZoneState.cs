using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingZoneState : PlayerState
{
    private BuildingManager m_BuildingManager;
    private string m_StructureName;

    public PlayerBuildingZoneState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.m_BuildingManager = buildingManager;
    }

    public override void OnConfirmAction()
    {
        
        this.m_BuildingManager.ConfirmModification();
        base.OnConfirmAction();
    }

    public override void OnCancle()
    {
        this.m_BuildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.m_BuildingManager.PrepareBuildingManager(this.GetType());
        this.m_StructureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {

        m_BuildingManager.PrepareStructureForPlacement(position, this.m_StructureName, StructureType.Zone);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        base.OnBuildSingleStructure(structureName);
        this.m_BuildingManager.CancelModification();
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        this.m_BuildingManager.PrepareStructureForPlacement(position, m_StructureName, StructureType.Zone);
    }

    public override void OnBuildRoad(string structureName)
    {

        base.OnBuildRoad(structureName);
        this.m_BuildingManager.CancelModification();
    }

    public override void OnInputPointerUp()
    {
        this.m_BuildingManager.StopContinuousPlacement();
    }
}
