using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="New zone structure", menuName ="CityBuilder/StructureData/ZoneStructure")]
public class ZoneStructureSO : StructureBaseSO
{
    [FormerlySerializedAs("upgradable")] public bool _upgradable;
    [FormerlySerializedAs("prefabVariants")] public GameObject[] _prefabVariants;
    [FormerlySerializedAs("availableUpgrades")] public UpgradeType[] _availableUpgrades;
    [FormerlySerializedAs("zoneType")] public ZoneType _zoneType;
}

[System.Serializable]
public struct UpgradeType
{
    [FormerlySerializedAs("prefabVariants")] public GameObject[] _prefabVariants;
    [FormerlySerializedAs("happinessThreshold")] public int _happinessThreshold;
    [FormerlySerializedAs("newIncome")] public int _newIncome;
    [FormerlySerializedAs("newUpkeep")] public int _newUpkeep;
}

public enum ZoneType
{
    Residential,
    Agricultural,
    Commercial
}
