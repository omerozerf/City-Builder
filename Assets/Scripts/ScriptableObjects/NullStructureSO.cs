using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullStructureSO : StructureBaseSO
{
    private void OnEnable()
    {
        _buildingName = "nullable object";
        _prefab = null;
        _placementCost = 0;
        _upkeepCost = 0;
        _income = 0;
        _requireRoadAccess = false;
        _requireWater = false;
        _requirePower = false;
    }

}
