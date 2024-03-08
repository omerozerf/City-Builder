using UnityEngine;

namespace ScriptableObjects
{
    public abstract class StructureBaseSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _placementCost;
        [SerializeField] private int _upkeepCost;
        [SerializeField] private int _income;
        [SerializeField] private bool _isRequireRoadAccess;
        [SerializeField] private bool _isRequireWater;
        [SerializeField] private bool _isRequirePower;
    
        public void SetName(string name)
        {
            _name = name;
        }
    
        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    
        public void SetPlacementCost(int placementCost)
        {
            _placementCost = placementCost;
        }
    
        public void SetUpkeepCost(int upkeepCost)
        {
            _upkeepCost = upkeepCost;
        }
    
        public void SetIncome(int income)
        {
            _income = income;
        }
    
        public void SetRequireRoadAccess(bool isRequireRoadAccess)
        {
            _isRequireRoadAccess = isRequireRoadAccess;
        }
    
        public void SetRequireWater(bool isRequireWater)
        {
            _isRequireWater = isRequireWater;
        }
    
        public void SetRequirePower(bool isRequirePower)
        {
            _isRequirePower = isRequirePower;
        }
    
        public string GetName()
        {
            return _name;
        }
    
        public GameObject GetPrefab()
        {
            return _prefab;
        }
    
        public int GetPlacementCost()
        {
            return _placementCost;
        }
    
        public int GetUpkeepCost()
        {
            return _upkeepCost;
        }
    
        public int GetIncome()
        {
            return _income;
        }
    
        public bool IsRequireRoadAccess()
        {
            return _isRequireRoadAccess;
        }
    
        public bool IsRequireWater()
        {
            return _isRequireWater;
        }
    
        public bool IsRequirePower()
        {
            return _isRequirePower;
        }
    }
}