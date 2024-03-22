using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationHelper
{
    private int m_Population = 0;

    public int Population
    {
        get { return m_Population; }
        private set { m_Population = value; }
    }

    public void AddToPopulation(int value)
    {
        Population += value;
    }

    public void ReducePopulation(int value)
    {
        Population -= value;
    }
}
