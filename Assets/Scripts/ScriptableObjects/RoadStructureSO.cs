using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New road structure", menuName = "CityBuilder/StructureData/RoadStructure")]
public class RoadStructureSO : StructureBaseSO
{
    [FormerlySerializedAs("cornerPrefab")] [Tooltip("Road facing up and right")]
    public GameObject _cornerPrefab;
    [FormerlySerializedAs("threeWayPrefab")] [Tooltip("Road facing up, right and down")]
    public GameObject _threeWayPrefab;
    [FormerlySerializedAs("fourWayPrefab")] public GameObject _fourWayPrefab;
    [FormerlySerializedAs("prefabRotation")] public RotationValue _prefabRotation = RotationValue.R0;
}

public enum RotationValue
{
    R0,
    R90,
    R180,
    R270
}