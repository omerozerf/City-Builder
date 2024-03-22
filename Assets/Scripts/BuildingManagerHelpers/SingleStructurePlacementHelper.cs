using UnityEngine;

namespace BuildingManagerHelpers
{
    public class SingleStructurePlacementHelper : StructureModificationHelper
    {
        public SingleStructurePlacementHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager, ResourceManager resourceManager) : base(structureRepository, grid, placementManager, resourceManager)
        {
        }

        public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
        {
            base.PrepareStructureForModification(inputPosition, structureName, structureType);
            //GameObject buildingPrefab = this.structureRepository.GetBuildingPrefabByName(structureName, structureType);
            GameObject buildingPrefab = structureData._prefab;
            Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            if (grid.IsCellTaken(gridPosition) == false)
            {
                if (structuresToBeModified.ContainsKey(gridPositionInt))
                {
                    RevokeStructurePlacementAt(gridPositionInt);

                }
                else
                {
                    PlaceNewStructureAt(buildingPrefab, gridPosition, gridPositionInt);
                }
            }
        }

        private void PlaceNewStructureAt(GameObject buildingPrefab, Vector3 gridPosition, Vector3Int gridPositionInt)
        {
            structuresToBeModified.Add(gridPositionInt, placementManager.CreateGhostStructure(gridPosition, buildingPrefab));
        }

        private void RevokeStructurePlacementAt(Vector3Int gridPositionInt)
        {
            var structure = structuresToBeModified[gridPositionInt];
            placementManager.DestroySingleStructure(structure);
            structuresToBeModified.Remove(gridPositionInt);
        }

    


    }
}
