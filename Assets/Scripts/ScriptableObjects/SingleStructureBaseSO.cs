using UnityEngine.Serialization;

namespace ScriptableObjects
{
    public abstract class SingleStructureBaseSO : StructureBaseSO
    {
        [FormerlySerializedAs("range")] public int _range;
    }
}
