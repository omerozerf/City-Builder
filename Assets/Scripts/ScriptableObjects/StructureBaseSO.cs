using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class StructureBaseSO : ScriptableObject
{
    [FormerlySerializedAs("buildingName")] public string _buildingName;
    [FormerlySerializedAs("prefab")] public GameObject _prefab;
    [FormerlySerializedAs("placementCost")] public int _placementCost;
    [FormerlySerializedAs("upkeepCost")] public int _upkeepCost;
    [FormerlySerializedAs("income")] public int _income;
    [FormerlySerializedAs("requireRoadAccess")] public bool _requireRoadAccess;
    [FormerlySerializedAs("requireWater")] public bool _requireWater;
    [FormerlySerializedAs("requirePower")] public bool _requirePower;
}
