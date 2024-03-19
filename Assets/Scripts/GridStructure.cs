using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructure
{
    private int m_CellSize;
    Cell[,] m_Grid;
    private int m_Width, m_Length;
    public GridStructure(int cellSize, int width, int length)
    {
        this.m_CellSize = cellSize;
        this.m_Width = width;
        this.m_Length = length;
        m_Grid = new Cell[this.m_Width,this.m_Length];
        for (int row = 0; row < m_Grid.GetLength(0); row++)
        {
            for (int col = 0; col < m_Grid.GetLength(1); col++)
            {
                m_Grid[row, col] = new Cell();
            }
        }
    }
    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / m_CellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / m_CellSize);
        return new Vector3(x * m_CellSize, 0, z * m_CellSize);
    }

    public void RemoveStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        m_Grid[cellIndex.y, cellIndex.x].RemoveStructure();
    }

    private Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)(gridPosition.x / m_CellSize), (int)(gridPosition.z / m_CellSize));
    }

    public bool IsCellTaken(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if(CheckIndexValidity(cellIndex))
            return m_Grid[cellIndex.y, cellIndex.x].IsTaken;
        throw new IndexOutOfRangeException("No index " + cellIndex + " in grid");
    }

    public void PlaceStructureOnTheGrid(GameObject structure, Vector3 gridPosition, StructureBaseSO structureData)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if (CheckIndexValidity(cellIndex))
            m_Grid[cellIndex.y, cellIndex.x].SetConstruction(structure, structureData);
    }

    public HashSet<Vector3Int> GetAllPositionsFromTo(Vector3Int minPoint, Vector3Int maxPoint)
    {
        HashSet<Vector3Int> positionsToReturn = new HashSet<Vector3Int>();
        for (int row = minPoint.z; row <= maxPoint.z; row++)
        {
            for (int col = minPoint.x; col <= maxPoint.x; col++)
            {
                Vector3 gridPositon = CalculateGridPosition(new Vector3(col, 0, row));
                positionsToReturn.Add(Vector3Int.FloorToInt(gridPositon));
            }
        }
        return positionsToReturn;
    }

    public StructureBaseSO GetStructureDataFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return m_Grid[cellIndex.y, cellIndex.x].GetStructureData();
    }

    private bool CheckIndexValidity(Vector2Int cellIndex)
    {
        if (cellIndex.x >= 0 && cellIndex.x < m_Grid.GetLength(1) && cellIndex.y >= 0 && cellIndex.y < m_Grid.GetLength(0))
            return true;
        return false;
    }

    public GameObject GetStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return m_Grid[cellIndex.y, cellIndex.x].GetStructure();
    }

    public Vector3Int? GetPositionOfTheNeighbourIfExists(Vector3 gridPosition, Direction direction)
    {
        Vector3Int? neighbourPosition = Vector3Int.FloorToInt(gridPosition);
        switch (direction)
        {
            case Direction.Up:
                neighbourPosition += new Vector3Int(0, 0, m_CellSize);
                break;
            case Direction.Right:
                neighbourPosition += new Vector3Int(m_CellSize, 0, 0);
                break;
            case Direction.Down:
                neighbourPosition += new Vector3Int(0, 0, -m_CellSize);
                break;
            case Direction.Left:
                neighbourPosition += new Vector3Int(-m_CellSize, 0, 0);
                break;
        }
        var index = CalculateGridIndex(neighbourPosition.Value);
        if (CheckIndexValidity(index) == false)
        {
            return null;
        }
        return neighbourPosition;
    }
}

public enum Direction
{
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
}