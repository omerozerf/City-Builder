using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New facility", menuName = "CityBuilder/StructureData/Facility")]
    public class SingleFacilitySO : SingleStructureBaseSO
    {
        [FormerlySerializedAs("maxCustomers")] public int _maxCustomers;
        [FormerlySerializedAs("upkeepPerCustomer")] public int _upkeepPerCustomer;
    }
}
