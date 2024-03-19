using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class SingleStructureBaseSO : StructureBaseSO
{
    [FormerlySerializedAs("range")] public int _range;
}
