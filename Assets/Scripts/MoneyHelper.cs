using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class MoneyHelper
{
    private int m_StartMoneyAmount;
    
    public MoneyHelper(int startMoneyAmount)
    {
        m_StartMoneyAmount = startMoneyAmount;
    }
    
    private void SetMoneyAmount(int startMoneyAmount)
    {
        if (startMoneyAmount >= 0)
        {
            m_StartMoneyAmount = startMoneyAmount;
        }
        else
        {
            m_StartMoneyAmount = 0;
            throw new MoneyException("Money amount cannot be negative");
        }
    }
    
    private void CollectIncome(IEnumerable<StructureBaseSO> buildings)
    {
        foreach (StructureBaseSO structure in buildings) 
        {
            AddMoneyAmount(structure._income); 
        }
    }

    private void ReduceUpkeep(IEnumerable<StructureBaseSO> buildings)
    {
        foreach (StructureBaseSO structure in buildings)
        {
            ReduceMoneyAmount(structure._upkeepCost);
        }
    }
    
    
    public void CalculateMoney(IEnumerable<StructureBaseSO> buildings)
    {
        CollectIncome(buildings);
        ReduceUpkeep(buildings);
    }
    
    public int GetMoneyAmount()
    {
        return m_StartMoneyAmount;
    }
    
    public void ReduceMoneyAmount(int amount)
    {
        SetMoneyAmount(GetMoneyAmount() - amount);
    }
    
    public void AddMoneyAmount(int amount)
    {
        SetMoneyAmount(GetMoneyAmount() + amount);
    }
}