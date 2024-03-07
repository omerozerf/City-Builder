using UnityEngine;

public class Cell
{
    private GameObject m_StructureModel = null;
    private bool m_IsTaken = false;
    
    
    public void SetStructure(GameObject structureModel)
    {
        if (!structureModel) return;
            
        m_StructureModel = structureModel;
        m_IsTaken = true;
    }
    
    public bool GetIsTaken()
    {
        return m_IsTaken;
    }
    
    public GameObject GetStructure()
    {
        return m_StructureModel;
    }
}