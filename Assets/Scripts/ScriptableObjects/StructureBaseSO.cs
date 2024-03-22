using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class StructureBaseSO : ScriptableObject
{
    [FormerlySerializedAs("buildingName")] public string _buildingName;
    [FormerlySerializedAs("prefab")] public GameObject _prefab;
    [FormerlySerializedAs("placementCost")] public int _placementCost;
    [FormerlySerializedAs("upkeepCost")] public int _upkeepCost;
    [FormerlySerializedAs("income")] [SerializeField]
    protected int _income;
    [FormerlySerializedAs("requireRoadAccess")] public bool _requireRoadAccess;
    [FormerlySerializedAs("requireWater")] public bool _requireWater;
    [FormerlySerializedAs("requirePower")] public bool _requirePower;
    [FormerlySerializedAs("structureRange")] public int _structureRange = 1;
    private SingleFacilitySO m_PowerProvider = null;
    private SingleFacilitySO m_WaterProvider = null;
    private RoadStructureSO m_RoadProvider = null;

    public SingleFacilitySO PowerProvider { get => m_PowerProvider;}
    public SingleFacilitySO WaterProvider { get => m_WaterProvider;}
    public RoadStructureSO RoadProvider { get => m_RoadProvider;}

    public virtual int GetIncome()
    {
        return _income;
    }

    public bool HasPower()
    {
        return m_PowerProvider != null;
    }

    public bool HasWater()
    {
        return m_PowerProvider != null;
    }

    public bool HasRoadAccess()
    {
        return m_RoadProvider != null;
    }

    internal void RemoveRoadProvider()
    {
        m_RoadProvider = null;
    }

    public void PreareStructure(IEnumerable<StructureBaseSO> structuresInRange)
    {
        AddRoadProvider(structuresInRange);
    }

    public void AddPowerFacility(SingleFacilitySO facility)
    {
        if (m_PowerProvider==null)
            m_PowerProvider = facility;
    }

    public virtual IEnumerable<StructureBaseSO> PrepareForDestruction()
    {
        if (m_PowerProvider != null)
            m_PowerProvider.RemoveClient(this);
        if (m_WaterProvider != null)
            m_WaterProvider.RemoveClient(this);
        return null;
    }
    public void AddWaterFacility(SingleFacilitySO facility)
    {
        if (m_WaterProvider==null)
            m_PowerProvider = facility;
    }

    public void RemovePowerFacility()
    {

        m_PowerProvider = null;


    }
    public void RemoveWaterFacility()
    {

        m_WaterProvider = null;

    }
    private void AddRoadProvider(IEnumerable<StructureBaseSO> structures)
    {
        if (m_RoadProvider != null)
            return;
        foreach (var nearbyStructure in structures)
        {
            if (nearbyStructure.GetType() == typeof(RoadStructureSO))
            {
                m_RoadProvider = (RoadStructureSO)nearbyStructure;
                return;
            }
        }
    }
}
