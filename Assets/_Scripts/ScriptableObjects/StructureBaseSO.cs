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
    
        protected void SetName(string name)
        {
            _name = name;
        }
    
        protected void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    
        protected void SetPlacementCost(int placementCost)
        {
            _placementCost = placementCost;
        }
    
        protected void SetUpkeepCost(int upkeepCost)
        {
            _upkeepCost = upkeepCost;
        }
    
        protected void SetIncome(int income)
        {
            _income = income;
        }
    
        protected void SetRequireRoadAccess(bool isRequireRoadAccess)
        {
            _isRequireRoadAccess = isRequireRoadAccess;
        }
    
        protected void SetRequireWater(bool isRequireWater)
        {
            _isRequireWater = isRequireWater;
        }
    
        protected void SetRequirePower(bool isRequirePower)
        {
            _isRequirePower = isRequirePower;
        }
    
        protected string GetName()
        {
            return _name;
        }
    
        protected GameObject GetPrefab()
        {
            return _prefab;
        }
    
        protected int GetPlacementCost()
        {
            return _placementCost;
        }
    
        protected int GetUpkeepCost()
        {
            return _upkeepCost;
        }
    
        protected int GetIncome()
        {
            return _income;
        }
    
        protected bool IsRequireRoadAccess()
        {
            return _isRequireRoadAccess;
        }
    
        protected bool IsRequireWater()
        {
            return _isRequireWater;
        }
    
        protected bool IsRequirePower()
        {
            return _isRequirePower;
        }
    }
}