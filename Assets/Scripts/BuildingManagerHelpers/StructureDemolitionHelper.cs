using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuildingManagerHelpers
{
    public class StructureDemolitionHelper : StructureModificationHelper
    {
        Dictionary<Vector3Int, GameObject> m_RoadToDemolish = new Dictionary<Vector3Int, GameObject>();
        public StructureDemolitionHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager, ResourceManager resourceManager) : base(structureRepository, grid, placementManager, resourceManager)
        {
        }
        public override void CancleModifications()
        {
            placementManager.PlaceStructuresOnTheMap(structuresToBeModified.Values);
            structuresToBeModified.Clear();
        }

        public override void ConfirmModifications()
        {
            foreach (var gridPosition in structuresToBeModified.Keys)
            {
                grid.RemoveStructureFromTheGrid(gridPosition);
            }
            foreach (var keyVeluPair in m_RoadToDemolish)
            {
                Dictionary<Vector3Int, GameObject> neighboursDictionary = RoadManager.GetRoadNeighboursForPosition(grid, keyVeluPair.Key);
                if (neighboursDictionary.Count > 0)
                {
                    var structureData = grid.GetStructureDataFromTheGrid(neighboursDictionary.Keys.First());
                    RoadManager.ModifyRoadCellsOnTheGrid(neighboursDictionary, structureData, null, grid, placementManager);
                }


            }
            placementManager.DestroyStructures(structuresToBeModified.Values);
            structuresToBeModified.Clear();
        }

        public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
        {
            Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
            if (grid.IsCellTaken(gridPosition))
            {
                var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
                var structure = grid.GetStructureFromTheGrid(gridPosition);
                if (structuresToBeModified.ContainsKey(gridPositionInt))
                {
                    RevokeStructureDemolitionAt(gridPositionInt, structure);
                }
                else
                {
                    AddStructureForDemolition(gridPositionInt, structure);
                }
            }
        }

        private void AddStructureForDemolition(Vector3Int gridPositionInt, GameObject structure)
        {
            structuresToBeModified.Add(gridPositionInt, structure);
            placementManager.SetBuildingForDemolition(structure);
            if (RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, gridPositionInt) && m_RoadToDemolish.ContainsKey(gridPositionInt) == false)
            {
                m_RoadToDemolish.Add(gridPositionInt, structure);
            }
        }

        private void RevokeStructureDemolitionAt(Vector3Int gridPositionInt, GameObject structure)
        {
            placementManager.ResetBuildingLook(structure);
            structuresToBeModified.Remove(gridPositionInt);
            if (RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, gridPositionInt) && m_RoadToDemolish.ContainsKey(gridPositionInt))
            {
                m_RoadToDemolish.Remove(gridPositionInt);
            }
        }

    }
}
