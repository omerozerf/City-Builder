using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ZoneStructure", menuName = "ScriptableObjects/StructureData/ZoneStructureSO")]
    public class ZoneStructureSO : StructureBaseSO
    {
        [SerializeField] private GameObject[] _prefabVariantArray;
        [SerializeField] private bool _isUpgradable;
        [SerializeField] private ZoneType _zoneType;
        
    }
    
    public enum ZoneType
    {
        Residential,
        Agricultural,
        Commercial,
    }

    [Serializable]
    public struct UpgradeType
    {
        [SerializeField] private GameObject[] _prefabVariantArray;
        [SerializeField] private int _happinessThreshold;
        [SerializeField] private int _newIncome;
        [SerializeField] private int _newUpkeep;
    }
}