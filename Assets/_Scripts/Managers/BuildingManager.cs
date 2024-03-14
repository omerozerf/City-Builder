using System.Collections.Generic;
using Types;
using UnityEngine;

namespace Managers
{
    public class BuildingManager
    {
        private GridStructure m_GridStructure;
        private PlacementManager m_PlacementManager;
        private StructureRepository m_StructureRepository;
        private Dictionary<Vector3Int, GameObject> m_StructuresToBeModifiedMap = new();

        public BuildingManager(PlacementManager placementManager, int cellSize, int width, int height,
            StructureRepository structureRepository)
        {
            m_PlacementManager = placementManager;
            m_GridStructure = new GridStructure(cellSize, width, height);
            m_StructureRepository = structureRepository;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void PlaceStructureAt(Vector3 position, string structureName, StructureType structureType)
        {
            GameObject buildingPrefab = m_StructureRepository.GetBuildingPrefabByName(structureName, structureType);
            Vector3 gridPosition = m_GridStructure.CalculateGridPosition(position);

            var gridPosInt = Vector3Int.FloorToInt(gridPosition);
            
            if (!m_GridStructure.IsCellTaken(gridPosition) && !m_StructuresToBeModifiedMap.ContainsKey(gridPosInt))
            {
                // m_PlacementManager.Build(gridPosition, m_GridStructure, buildingPrefab);

                GameObject structure = m_PlacementManager.CreateGhostStructure(gridPosition, buildingPrefab);
                m_StructuresToBeModifiedMap.Add(gridPosInt, structure);
            }
        }

        public void ConfirmPlacement()
        {
            m_PlacementManager.ConfirmPlacement(m_StructuresToBeModifiedMap.Values);

            foreach (var keyValuePair in m_StructuresToBeModifiedMap)
            {
                m_GridStructure.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key);
            }
            m_StructuresToBeModifiedMap.Clear();
        }

        public void CancelPlacement()
        {
            m_PlacementManager.CancelPlacement(m_StructuresToBeModifiedMap.Values);
            m_StructuresToBeModifiedMap.Clear();
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