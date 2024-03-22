using ScriptableObjects;
using UnityEngine;

namespace BuildingManagerHelpers
{
    public class RoadStructureHelper
    {
        public RotationValue RoadPrefabRotation { get; set; }
        public GameObject RoadPrefab { get; set; }

        public RoadStructureHelper(GameObject roadPrefab, RotationValue roadPrefabRotation)
        {

            RoadPrefabRotation = roadPrefabRotation;

            RoadPrefab = roadPrefab;
        }
    }
}