using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Collection", menuName = "ScriptableObjects/CollectionSO")]
    public class CollectionSO : ScriptableObject
    {
        [SerializeField] private RoadStructureSO _roadStructure;
        [SerializeField] private List<SingleStructureBaseSO> _singleStructureList;
        [SerializeField] private List<ZoneStructureSO> _zoneStructureList;
        
        
        public RoadStructureSO GetRoadStructure()
        {
            return _roadStructure;
        }
        
        public List<SingleStructureBaseSO> GetSingleStructureList()
        {
            return _singleStructureList;
        }
        
        public List<ZoneStructureSO> GetZoneStructureList()
        {
            return _zoneStructureList;
        }
    }
}