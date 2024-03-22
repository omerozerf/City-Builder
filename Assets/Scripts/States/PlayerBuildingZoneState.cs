using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingZoneState : PlayerState
{
    private BuildingManager m_BuildingManager;
    private string m_StructureName;

    public PlayerBuildingZoneState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        m_BuildingManager = buildingManager;
    }

    public override void OnConfirmAction()
    {
        
        m_BuildingManager.ConfirmModification();
        base.OnConfirmAction();
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

    public override void OnInputPointerDown(Vector3 position)
    {

        m_BuildingManager.PrepareStructureForPlacement(position, m_StructureName, StructureType.Zone);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        base.OnBuildSingleStructure(structureName);
        m_BuildingManager.CancelModification();
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        m_BuildingManager.PrepareStructureForPlacement(position, m_StructureName, StructureType.Zone);
    }

    public override void OnBuildRoad(string structureName)
    {

        base.OnBuildRoad(structureName);
        m_BuildingManager.CancelModification();
    }

    public override void OnInputPointerUp()
    {
        m_BuildingManager.StopContinuousPlacement();
    }
}
