using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New collection", menuName = "CityBuilder/CollectionSO")]
public class CollectionSO : ScriptableObject
{
    [FormerlySerializedAs("roadStructure")] public RoadStructureSO _roadStructure;
    [FormerlySerializedAs("singleStructureList")] public List<SingleStructureBaseSO> _singleStructureList;
    [FormerlySerializedAs("zonesList")] public List<ZoneStructureSO> _zonesList;
}
