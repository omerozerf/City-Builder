using Types;
using UnityEngine;

namespace Managers
{
    public class BuildingManager
    {
        private GridStructure m_GridStructure;
        private PlacementManager m_PlacementManager;
        private StructureRepository m_StructureRepository;

        public BuildingManager(PlacementManager placementManager, int cellSize, int width, int height,
            StructureRepository structureRepository)
        {
            m_PlacementManager = placementManager;
            m_GridStructure = new GridStructure(cellSize, width, height);
            m_StructureRepository = structureRepository;
        }

        public void PlaceStructureAt(Vector3 position, string structureName, StructureType structureType)
        {
            GameObject buildingPrefab = m_StructureRepository.GetBuildingPrefabByName(structureName, structureType);
            Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);
        
            Debug.Log(buildingPrefab);
            
            if (!m_GridStructure.IsCellTaken(gridPosition))
            {
                m_PlacementManager.Build(gridPosition, m_GridStructure, buildingPrefab);
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
}