using System;
using UnityEngine;

public class GridStructure
{
    private readonly int m_CellSize;
    private readonly int m_Width;
    private readonly int m_Height;
    private readonly Cell[,] m_Grid;
        
    public GridStructure(int cellSize, int width, int height)
    {
        m_CellSize = cellSize;
        m_Width = width;
        m_Height = height;
            
        m_Grid = new Cell[m_Width, m_Height];

        for (int row = 0; row < m_Grid.GetLength(0); row++)
        {
            for (int col = 0; col < m_Grid.GetLength(1); col++)
            {
                m_Grid[row, col] = new Cell();
            }
        }
    }

    private Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        float x = gridPosition.x / m_CellSize;
        float z = gridPosition.z / m_CellSize;
            
        return new Vector2Int( (int)x * m_CellSize, (int)z * m_CellSize);
    }
        
    private bool CheckIndexValidity(Vector2Int index)
    {
        return index.x >= 0 &&
               index.x < m_Grid.GetLength(1) &&
               index.y >= 0 && 
               index.y < m_Grid.GetLength(0);
    }
        
        
    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float) inputPosition.x / m_CellSize);
        int z = Mathf.FloorToInt((float) inputPosition.z / m_CellSize);
            
        return new Vector3(x * m_CellSize, 0, z * m_CellSize);
    }
        
    public bool IsCellTaken(Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
            
        if (CheckIndexValidity(cellIndex)) return m_Grid[cellIndex.y, cellIndex.x].GetIsTaken();
            
        throw new IndexOutOfRangeException($"No index {cellIndex} in grid");
    }

    public void PlaceStructureOnTheGrid(GameObject structure, Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
            
        if (CheckIndexValidity(cellIndex)) m_Grid[cellIndex.y, cellIndex.x].SetStructure(structure);
    }
    
    public void RemoveStructureFromTheGrid(Vector3 position)
    {
        Vector2Int cellIndex = CalculateGridIndex(position);
        
        m_Grid[cellIndex.y, cellIndex.x].RemoveStructure();
    }
    
    public GameObject GetStructureFromTheGrid(Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
        GameObject currentObj = m_Grid[cellIndex.y, cellIndex.x].GetStructure();

        return currentObj;
    }
}