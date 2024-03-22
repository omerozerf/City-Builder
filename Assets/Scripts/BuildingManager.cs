using System;
using System.Collections;
using System.Collections.Generic;
using BuildingManagerHelpers;
using ScriptableObjects;
using UnityEngine;

public class BuildingManager
{
    GridStructure m_Grid;
    IPlacementManager m_PlacementManager;
    StructureRepository m_StructureRepository;
    StructureModificationHelper m_Helper;

    public BuildingManager(int cellSize, int width, int length, IPlacementManager placementManager, StructureRepository structureRepository, ResourceManager resourceManager)
    {
        m_Grid = new GridStructure(cellSize, width, length);
        m_PlacementManager = placementManager;
        m_StructureRepository = structureRepository;
        StructureModificationFactory.PrepareFactory(structureRepository, m_Grid, placementManager, resourceManager);

    }

    public void PrepareBuildingManager(Type classType)
    {
        m_Helper = StructureModificationFactory.GetHelper(classType);
    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        m_Helper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void ConfirmModification()
    {
        m_Helper.ConfirmModifications();
    }

    public void CancelModification()
    {
        m_Helper.CancleModifications();
    }

    public void PrepareStructureForDemolitionAt(Vector3 inputPosition)
    {
        m_Helper.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }

    public GameObject CheckForStructureInGrid(Vector3 inputPosition)
    {
        Vector3 gridPositoion = m_Grid.CalculateGridPosition(inputPosition);
        if (m_Grid.IsCellTaken(gridPositoion))
        {
            return m_Grid.GetStructureFromTheGrid(gridPositoion);
        }
        return null;
    }

    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = m_Grid.CalculateGridPosition(inputPosition);
        GameObject structureToReturn = null;
        structureToReturn = m_Helper.AccessStructureInDictionary(gridPosition);
        if (structureToReturn != null)
        {
            return structureToReturn;
        }
        structureToReturn = m_Helper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }

    public void StopContinuousPlacement()
    {
        m_Helper.StopContinuousPlacement();
    }

    public IEnumerable<StructureBaseSO> GetAllStructures()
    {
        return m_Grid.GetAllStructures();
    }
}
