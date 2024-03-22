using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuildingManagerHelpers
{
    public class ZonePlacementHelper : StructureModificationHelper
    {
        private Vector3 m_StartPosition;
        private Vector3? m_PreviousEndPositon = null;
        private bool m_StartPositionAcquired = false;
        private Vector3 m_MapBottomLeftCorner;
        private Queue<GameObject> m_GameObjectsToReuse = new Queue<GameObject>();
        private int m_StructuresOldQuantity = 0;
        
        public ZonePlacementHelper(StructureRepository structureRepository, GridStructure grid,
            IPlacementManager placementManager, Vector3 mapBottomLeftCorner, ResourceManager resourceManager) : base(
            structureRepository, grid, placementManager, resourceManager)
        {
            m_MapBottomLeftCorner = mapBottomLeftCorner;
        }

        public override void PrepareStructureForModification(Vector3 inputPosition, string structureName,
            StructureType structureType)
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
                PlaceNewZoneUpToPosition(gridPositon);
            }

        }

        private void PlaceNewZoneUpToPosition(Vector3 endPosition)
        {
            Vector3Int minPoint = Vector3Int.FloorToInt(m_StartPosition);
            Vector3Int maxPoint = Vector3Int.FloorToInt(endPosition);

            ZoneCalculator.PrepareStartAndEndPosition(m_StartPosition, endPosition, ref minPoint, ref maxPoint,
                m_MapBottomLeftCorner);
            HashSet<Vector3Int> newPositionsSet = grid.GetAllPositionsFromTo(minPoint, maxPoint);

            newPositionsSet = CalculateZoneCost(newPositionsSet);
            
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
                    structureToAdd = placementManager.MoveStructureOnTheMap(positionToPlaceStructure, gameObjectToReuse,
                        structureData._prefab);

                }
                else
                {
                    structureToAdd = placementManager.CreateGhostStructure(positionToPlaceStructure, structureData._prefab);

                }
                structuresToBeModified.Add(positionToPlaceStructure, structureToAdd);
            }


        }

        private HashSet<Vector3Int> CalculateZoneCost(HashSet<Vector3Int> newPositionsSet)
        {
            resourceManager.AddMoney(m_StructuresOldQuantity * structureData._placementCost);
            int numberOfZonesToPlace =
                resourceManager.HowManyStructuresCanBePlaced(structureData._placementCost, newPositionsSet.Count);

            if (numberOfZonesToPlace < newPositionsSet.Count)
            {
                newPositionsSet = new HashSet<Vector3Int>(newPositionsSet.Take(numberOfZonesToPlace).ToList());
            }
            
            m_StructuresOldQuantity = newPositionsSet.Count;
            resourceManager.SpendMoney(m_StructuresOldQuantity * structureData._placementCost);
            
            return newPositionsSet;
        }

        public override void CancleModifications()
        {
            resourceManager.AddMoney(m_StructuresOldQuantity * structureData._placementCost);
            
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
            m_StructuresOldQuantity = 0;
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
}
