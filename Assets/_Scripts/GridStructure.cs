using UnityEngine;

namespace _Scripts
{
    public class GridStructure
    {
        private readonly int m_CellSize;

        public GridStructure(int cellSize)
        {
            m_CellSize = cellSize;
        }

        public Vector3 CalculateGridPosition(Vector3 inputPosition)
        {
            int x = Mathf.FloorToInt((float) inputPosition.x / m_CellSize);
            int z = Mathf.FloorToInt((float) inputPosition.z / m_CellSize);
            
            return new Vector3(x * m_CellSize, 0, z * m_CellSize);
        }
    }
}
