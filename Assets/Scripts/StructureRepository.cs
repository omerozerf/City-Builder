using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class StructureRepository : MonoBehaviour
{
    [FormerlySerializedAs("modelDataCollection")] public CollectionSO _modelDataCollection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public List<string> GetZoneNames()
    {
        return _modelDataCollection._zonesList.Select(zone => zone._buildingName).ToList();
    }

    public List<string> GetSingleStructureNames()
    {
        return _modelDataCollection._singleStructureList.Select(facility => facility._buildingName).ToList();
    }

    public string GetRoadStructureName()
    {
        return _modelDataCollection._roadStructure._buildingName;
    }

    public GameObject GetBuildingPrefabByName(string structureName, StructureType structureType)
    {
        GameObject structurePrefabToReturn = null;
        switch (structureType)
        {
            case StructureType.Zone:
                structurePrefabToReturn = GetZoneBuildingPrefabByName(structureName);
                break;
            case StructureType.SingleStructure:
                structurePrefabToReturn = GetSingleStructureBuildingPrefabByName(structureName);
                break;
            case StructureType.Road:
                structurePrefabToReturn = GetRoadBuildingPrefab();
                break;
            default:
                throw new Exception("No such type. not implemented for " + structureType);
        }

        if (structurePrefabToReturn == null)
        {
            throw new Exception("No prefab for that name " + structureName);
        }
        return structurePrefabToReturn;
    }

    private GameObject GetRoadBuildingPrefab()
    {
        return _modelDataCollection._roadStructure._prefab;
    }

    public StructureBaseSO GetStructureData(string structureName, StructureType structureType)
    {
        switch (structureType)
        {
            case StructureType.Zone:
                return _modelDataCollection._zonesList.Where(structure => structure._buildingName == structureName).FirstOrDefault();
            case StructureType.SingleStructure:
                return _modelDataCollection._singleStructureList.Where(structure => structure._buildingName == structureName).FirstOrDefault();
            case StructureType.Road:
                return _modelDataCollection._roadStructure;
            default:
                throw new Exception("No such type. not implemented for " + structureType);
        }
    }

    private GameObject GetSingleStructureBuildingPrefabByName(string structureName)
    {
        var foundStructure = _modelDataCollection._singleStructureList.Where(structure => structure._buildingName == structureName).FirstOrDefault();
        if (foundStructure != null)
        {
            return foundStructure._prefab;
        }
        return null;
    }

    private GameObject GetZoneBuildingPrefabByName(string structureName)
    {
        var foundStructure = _modelDataCollection._zonesList.Where(structure => structure._buildingName == structureName).FirstOrDefault();
        if (foundStructure != null)
        {
            return foundStructure._prefab;
        }
        return null;
    }
}

public enum StructureType
{
    None,
    Zone,
    SingleStructure,
    Road
}

