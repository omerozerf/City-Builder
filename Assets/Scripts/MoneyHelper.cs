using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHelper
{
    private int m_Money;

    public MoneyHelper(int startMoneyAmount)
    {
        m_Money = startMoneyAmount;
    }

    public int Money { get => m_Money; 
        private set 
        { 
            if(value < 0)
            {
                m_Money = 0;
                throw new MoneyException("Not enough money");
            }
            else
            {
                m_Money = value;
            }
            
        } 
    }

    public void ReduceMoney(int amount)
    {
        Money -= amount;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public void CalculateMoney(IEnumerable<StructureBaseSO> buildings)
    {
        CollectIncome(buildings);
        ReduceUpkeep(buildings);
    }

    private void ReduceUpkeep(IEnumerable<StructureBaseSO> buildings)
    {
        foreach (var structure in buildings)
        {
            Money -= structure._upkeepCost;
        }
    }

    private void CollectIncome(IEnumerable<StructureBaseSO> buildings)
    {
        foreach (var structure in buildings)
        {
            Money += structure.GetIncome();
        }
    }
}
