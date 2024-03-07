using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "RoadStructure", menuName = "ScriptableObjects/StructureData/RoadStructureSO")]
    public class RoadStructureSO : StructureBaseSO
    {
        [Tooltip("Road facing up and right.")]
        [SerializeField] private GameObject _curnerPrefab;
        [Tooltip("Road facing up, down and right.")]
        [SerializeField] private GameObject _threeWayPrefab;
        [SerializeField] private GameObject _fourWayPrefab;
    }
}