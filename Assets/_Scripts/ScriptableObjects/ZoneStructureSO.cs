using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ZoneStructure", menuName = "ScriptableObjects/StructureData/ZoneStructureSO")]
    public class ZoneStructureSO : StructureBaseSO
    {
        [SerializeField] private GameObject[] _prefabVariantArray;
        [SerializeField] private bool _isUpgradable;
        [SerializeField] private UpgradeType[] _upgradeTypeArray;
        [SerializeField] private ZoneType _zoneType;
    }
}