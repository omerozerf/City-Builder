using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureModificationFactory
{
    private static StructureModificationHelper ms_SingleStructurePlacementHelper;
    private static StructureModificationHelper ms_StructureDemolitionHelper;
    private static StructureModificationHelper ms_RoadStructurePlacementHelper;
    private static StructureModificationHelper ms_ZonePlacementHelper;
    public static void PrepareFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager)
    {
        ms_SingleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager);
        ms_StructureDemolitionHelper = new StructureDemolitionHelper(structureRepository, grid, placementManager);
        ms_RoadStructurePlacementHelper = new RoadPlacementModificationHelper(structureRepository, grid, placementManager);
        ms_ZonePlacementHelper = new ZonePlacementHelper(structureRepository, grid, placementManager, Vector3.zero);
    }

    public static StructureModificationHelper GetHelper(Type classType)
    {
        if (classType == typeof(PlayerDemolitionState))
        {
            return ms_StructureDemolitionHelper;
        }
        else if (classType == typeof(PlayerBuildingZoneState))
        {
            return ms_ZonePlacementHelper;
        }
        else if (classType == typeof(PlayerBuildingRoadState))
        {
            return ms_RoadStructurePlacementHelper;
        }
        else
        {
            return ms_SingleStructurePlacementHelper;
        }
    }
}
