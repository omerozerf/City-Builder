using System;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public struct UpgradeType
    {
        [SerializeField] private GameObject[] _prefabVariantArray;
        [SerializeField] private int _happinessThreshold;
        [SerializeField] private int _newIncome;
        [SerializeField] private int _newUpkeep;
    }
}