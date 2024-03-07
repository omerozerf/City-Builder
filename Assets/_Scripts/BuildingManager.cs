using UnityEngine;

public class BuildingManager
{
    private GridStructure m_GridStructure;
    private PlacementManager m_PlacementManager;

    public BuildingManager(PlacementManager placementManager, int cellSize, int width, int height)
    {
        m_PlacementManager = placementManager;
        
        m_GridStructure = new GridStructure(cellSize, width, height);
    }

    public void PlaceStructureAt(Vector3 position)
    {
        Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);
        
        if (!m_GridStructure.IsCellTaken(gridPosition))
        {
            m_PlacementManager.Build(gridPosition, m_GridStructure);
        }
    }

    public void RemoveBuildingAt(Vector3 position)
    {
        Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);
        
        if (m_GridStructure.IsCellTaken(gridPosition))
        {
            m_PlacementManager.RemoveBuilding(gridPosition, m_GridStructure);
        }
    }
}