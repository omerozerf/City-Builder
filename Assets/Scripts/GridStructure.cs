using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructure
{
    private int m_CellSize;
    private Cell[,] m_Grid;
    private int m_Width, m_Length;
    public GridStructure(int cellSize, int width, int length)
    {
        m_CellSize = cellSize;
        m_Width = width;
        m_Length = length;
        m_Grid = new Cell[m_Width,m_Length];
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

    internal IEnumerable<Vector3Int> GetStructurePositionsInRange(Vector3Int gridPosition, int range)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        List<Vector3Int> listToReturn = new List<Vector3Int>();
        if (CheckIndexValidity(cellIndex) == false)
            return listToReturn;
        for (int row = cellIndex.y - range; row <= cellIndex.y + range; row++)
        {
            for (int col = cellIndex.x - range; col <= cellIndex.x + range; col++)
            {
                var tempPosition = new Vector2Int(col, row);
                if (CheckIndexValidity(tempPosition) && Vector2.Distance(cellIndex, tempPosition) <= range)
                {
                    var data = m_Grid[row, col].GetStructureData();
                    if (data != null)
                    {
                        listToReturn.Add(GetGridPositionFromIndex(tempPosition));
                    }
                }
            }
        }
        return listToReturn;
    }

    internal bool ArePositionsInRange(Vector3Int gridPosition, Vector3Int structurePositionNearby, int range)
    {
        var distance = Vector2.Distance(CalculateGridIndex(gridPosition), CalculateGridIndex(structurePositionNearby));
        return distance <= range;
    }

    private Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)(gridPosition.x / m_CellSize), (int)(gridPosition.z / m_CellSize));
    }

    private Vector3Int GetGridPositionFromIndex(Vector2Int tempPosition)
    {
        return new Vector3Int(tempPosition.x * m_CellSize, 0, tempPosition.y * m_CellSize);
    }

    public IEnumerable<StructureBaseSO> GetAllStructures()
    {
        List<StructureBaseSO> structureData = new List<StructureBaseSO>();
        for (int row = 0; row < m_Grid.GetLength(0); row++)
        {
            for (int col = 0; col < m_Grid.GetLength(1); col++)
            {
                var data = m_Grid[row, col].GetStructureData();
                if (data != null)
                {
                    structureData.Add(data);
                }
            }
        }
        return structureData;
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

    public IEnumerable<StructureBaseSO> GetStructuresDataInRange(Vector3 gridPosition, int range)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        List<StructureBaseSO> listToReturn = new List<StructureBaseSO>();
        if (CheckIndexValidity(cellIndex) == false)
        {
            return listToReturn;
        }
        for (int row = cellIndex.y - range; row <= cellIndex.y + range; row++)
        {
            for (int col = cellIndex.x - range; col <= cellIndex.x + range; col++)
            {
                var tempPosition = new Vector2Int(col, row);
                if (CheckIndexValidity(tempPosition) && Vector2.Distance(cellIndex, tempPosition) <= range)
                {
                    var data = m_Grid[row, col].GetStructureData();
                    if (data != null)
                    {
                        listToReturn.Add(data);
                    }
                }
            }
        }
        return listToReturn;
    }
}

public enum Direction
{
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
}