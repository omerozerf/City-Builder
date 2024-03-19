using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    GameObject m_StructureModel = null;
    StructureBaseSO m_StructureData;
    bool m_İsTaken = false;

    public bool IsTaken { get => m_İsTaken; }

    public void SetConstruction(GameObject structureModel, StructureBaseSO structureData)
    {
        if (structureModel == null)
            return;
        this.m_StructureModel = structureModel;
        this.m_İsTaken = true;
        this.m_StructureData = structureData;
    }

    public GameObject GetStructure()
    {
        return m_StructureModel;
    }
    public void RemoveStructure()
    {
        m_StructureModel = null;
        m_İsTaken = false;
        m_StructureData = null;
    }

    public StructureBaseSO GetStructureData()
    {
        return m_StructureData;
    }
}
