﻿using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace BuildingManagerHelpers
{
    public abstract class StructureModificationHelper
    {
        protected Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();
        protected readonly StructureRepository structureRepository;
        protected readonly GridStructure grid;
        protected readonly IPlacementManager placementManager;
        protected StructureBaseSO structureData;
        protected ResourceManager resourceManager;

        public StructureModificationHelper(StructureRepository structureRepository, GridStructure grid,
            IPlacementManager placementManager, ResourceManager resourceManager)
        {
            this.structureRepository = structureRepository;
            this.grid = grid;
            this.placementManager = placementManager;
            this.resourceManager = resourceManager;
        }

        public GameObject AccessStructureInDictionary(Vector3 gridPosition)
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            if (structuresToBeModified.ContainsKey(gridPositionInt))
            {
                return structuresToBeModified[gridPositionInt];
            }
            return null;
        }

        public virtual void ConfirmModifications()
        {
            placementManager.PlaceStructuresOnTheMap(structuresToBeModified.Values);
            foreach (var keyValuePair in structuresToBeModified)
            {
                grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key, Object.Instantiate(structureData) );
            }
            ResetHelpersData();
        }

        public virtual void CancleModifications()
        {
            placementManager.DestroyStructures(structuresToBeModified.Values);
            ResetHelpersData();
        }
        public virtual void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
        {
            if (structureData == null && structureType != StructureType.None)
            {
                structureData = structureRepository.GetStructureData(structureName, structureType);
            }
        }

        protected void ResetHelpersData()
        {
            structureData = null;
            structuresToBeModified.Clear();
        }

        public virtual void StopContinuousPlacement()
        {

        }
    }
}
