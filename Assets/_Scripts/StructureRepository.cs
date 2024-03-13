using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Types;
using UnityEngine;
using UnityEngine.Serialization;

public class StructureRepository : MonoBehaviour
{
    [FormerlySerializedAs("_modelCollectionData")] 
    [SerializeField] private CollectionSO _modelDataCollection;


    public List<string> GetZonesNameList()
    {
        return _modelDataCollection.GetZoneStructureList().Select(zoneStructure => zoneStructure.GetName()).ToList();
    }
    
    public List<string> GetFacilityNameList()
    {
        return _modelDataCollection.GetSingleStructureList().Select(singleStructure => singleStructure.GetName()).ToList();
    }
    
    public string GetRoadStructureName()
    {
        return _modelDataCollection.GetRoadStructure().GetName();
    }

    public GameObject GetBuildingPrefabByName(string structureName, StructureType structureType)
    {
        return structureType switch
        {
            StructureType.Zone => GetZoneBuildingPrefabByName(structureName),
            
            StructureType.Facility => GetFacilityBuildingPrefabByName(structureName),
            
            StructureType.Road => GetRoadBuildingPrefabByName(structureName),
            
            _ => throw new ArgumentOutOfRangeException(nameof(structureType), structureType,
                "No such structure type found.")
        };
    }

    
    private GameObject GetFacilityBuildingPrefabByName(string structureName)
    {
        //TODO: Implement logic to get the prefab by name
        return _modelDataCollection.GetSingleStructureList()[0].GetPrefab();
    }

    private GameObject GetRoadBuildingPrefabByName(string structureName)
    {
        return _modelDataCollection.GetRoadStructure().GetPrefab();
    }

    private GameObject GetZoneBuildingPrefabByName(string structureName)
    {
        //TODO: Implement logic to get the prefab by name
        return _modelDataCollection.GetZoneStructureList()[0].GetPrefab();
    }
}