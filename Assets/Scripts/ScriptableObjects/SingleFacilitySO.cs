using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New facility", menuName = "CityBuilder/StructureData/Facility")]
public class SingleFacilitySO : SingleStructureBaseSO
{
    [FormerlySerializedAs("maxCustomers")] public int _maxCustomers;
    [FormerlySerializedAs("upkeepPerCustomer")] public int _upkeepPerCustomer;
    private HashSet<StructureBaseSO> m_Customers = new HashSet<StructureBaseSO>();
    [FormerlySerializedAs("facilityType")] public FacilityType _facilityType = FacilityType.None;

    public void RemoveClient(StructureBaseSO clientStructure)
    {
        if (m_Customers.Contains(clientStructure))
        {
            if (_facilityType == FacilityType.Water)
            {
                clientStructure.RemoveWaterFacility();
            }
            if (_facilityType == FacilityType.Power)
            {
                clientStructure.RemovePowerFacility();
            }
            m_Customers.Remove(clientStructure);
        }
    }

    public override int GetIncome()
    {
        return m_Customers.Count * _income;
    }


    internal void AddClients(IEnumerable<StructureBaseSO> structuresAroundFacility)
    {
        foreach (var nearbyStructure in structuresAroundFacility)
        {
            if (_maxCustomers > m_Customers.Count && nearbyStructure != this)
            {
                if (_facilityType == FacilityType.Water && nearbyStructure._requireWater)
                {
                    nearbyStructure.AddWaterFacility(this);
                    m_Customers.Add(nearbyStructure);
                }
                if (_facilityType == FacilityType.Power && nearbyStructure._requirePower)
                {
                    nearbyStructure.AddPowerFacility(this);
                    m_Customers.Add(nearbyStructure);
                }
            }
        }
    }

    public override IEnumerable<StructureBaseSO> PrepareForDestruction()
    {
        base.PrepareForDestruction();
        List<StructureBaseSO> tempList = new List<StructureBaseSO>(m_Customers);
        foreach(var clientStructure in tempList)
        {
            RemoveClient(clientStructure);
        }
        m_Customers.Clear();
        return tempList;
    }

    internal bool IsFull()
    {
        return GetNumberOfCustomers() > _maxCustomers;
    }

    public int GetNumberOfCustomers()
    {
        return m_Customers.Count;
    }
}


public enum FacilityType
{
    Power,
    Water,
    None
}
