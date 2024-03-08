using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class StructureRepository : MonoBehaviour
{
    [FormerlySerializedAs("_modelCollectionData")] [SerializeField] private CollectionSO _modelDataCollection;


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
}
