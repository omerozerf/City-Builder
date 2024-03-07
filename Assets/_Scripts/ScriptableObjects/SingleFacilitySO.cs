using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SingleFacility", menuName = "ScriptableObjects/StructureData/SingleFacilitySO")]
    public class SingleFacilitySO : SingleStructureBaseSO
    {
        [SerializeField] private int _maxCustomers;
        [SerializeField] private int _upkeepCostPerCustomer;
    }
}