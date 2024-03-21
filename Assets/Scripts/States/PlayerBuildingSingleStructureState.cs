using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    private BuildingManager m_BuildingManager;
    private string m_StructureName;
    public PlayerBuildingSingleStructureState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.m_BuildingManager = buildingManager;
    }
    public override void OnConfirmAction()
    {
        base.OnConfirmAction();
        this.m_BuildingManager.ConfirmModification();
    }
    public override void OnInputPointerDown(Vector3 position)
    {

        m_BuildingManager.PrepareStructureForPlacement(position, this.m_StructureName, StructureType.SingleStructure);
    }

    public override void OnBuildArea(string structureName)
    {
        
        base.OnBuildArea(structureName);
        this.m_BuildingManager.CancelModification();
    }

    public override void OnBuildRoad(string structureName)
    {
        
        base.OnBuildRoad(structureName);
        this.m_BuildingManager.CancelModification();
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
}
