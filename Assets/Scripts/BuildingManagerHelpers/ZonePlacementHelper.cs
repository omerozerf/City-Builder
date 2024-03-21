using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePlacementHelper : StructureModificationHelper
{
    Vector3 m_StartPosition;
    Vector3? m_PreviousEndPositon = null;
    bool m_StartPositionAcquired = false;
    Vector3 m_MapBottomLeftCorner;
    Queue<GameObject> m_GameObjectsToReuse = new Queue<GameObject>();
    public ZonePlacementHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager, Vector3 mapBottomLeftCorner) : base(structureRepository, grid, placementManager)
    {
        m_MapBottomLeftCorner = mapBottomLeftCorner;
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPositon = grid.CalculateGridPosition(inputPosition);
        if (m_StartPositionAcquired == false && grid.IsCellTaken(gridPositon) == false)
        {
            m_StartPosition = gridPositon;
            m_StartPositionAcquired = true;
        }
        if (m_StartPositionAcquired && m_PreviousEndPositon == null || ZoneCalculator.CheckIfPositionHasChanged(gridPositon, m_PreviousEndPositon.Value, grid))
        {
            PlaceNewZoneUpTo(gridPositon);
        }

    }

    private void PlaceNewZoneUpTo(Vector3 endPosition)
    {
        Vector3Int minPoint = Vector3Int.FloorToInt(m_StartPosition);
        Vector3Int maxPoint = Vector3Int.FloorToInt(endPosition);

        ZoneCalculator.PrepareStartAndEndPosition(m_StartPosition, endPosition, ref minPoint, ref maxPoint, m_MapBottomLeftCorner);
        HashSet<Vector3Int> newPositionsSet = grid.GetAllPositionsFromTo(minPoint, maxPoint);
        m_PreviousEndPositon = endPosition;
        ZoneCalculator.CalculateZone(newPositionsSet, structuresToBeModified, m_GameObjectsToReuse);

        foreach (var positionToPlaceStructure in newPositionsSet)
        {
            if (grid.IsCellTaken(positionToPlaceStructure))
                continue;
            GameObject structureToAdd = null;
            if (m_GameObjectsToReuse.Count > 0)
            {
                var gameObjectToReuse = m_GameObjectsToReuse.Dequeue();
                gameObjectToReuse.SetActive(true);
                structureToAdd = placementManager.MoveStructureOnTheMap(positionToPlaceStructure, gameObjectToReuse, structureData._prefab);

            }
            else
            {
                structureToAdd = placementManager.CreateGhostStructure(positionToPlaceStructure, structureData._prefab);

            }
            structuresToBeModified.Add(positionToPlaceStructure, structureToAdd);
        }


    }

    public override void CancleModifications()
    {
        base.CancleModifications();
        ResetZonePlacementHelper();
    }

    public override void ConfirmModifications()
    {
        base.ConfirmModifications();
        ResetZonePlacementHelper();
    }

    private void ResetZonePlacementHelper()
    {
        placementManager.DestroyStructures(m_GameObjectsToReuse);
        m_GameObjectsToReuse.Clear();
        m_StartPositionAcquired = false;
        m_PreviousEndPositon = null;
    }

    public override void StopContinuousPlacement()
    {
        m_StartPositionAcquired = false;
        base.StopContinuousPlacement();
    }

}
