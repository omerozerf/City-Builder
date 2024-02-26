using UnityEngine;

namespace _Scripts
{
    public class GridStructure : MonoBehaviour
    {
        private const int CELL_SIZE = 2;
        
        private Vector3 CalculateGridPosition(Vector3 inputPosition)
        {
            int x = Mathf.FloorToInt((float) inputPosition.x / CELL_SIZE);
            int z = Mathf.FloorToInt((float) inputPosition.z / CELL_SIZE);
            
            return new Vector3(x * CELL_SIZE, 0, z * CELL_SIZE);
        }
    }
}
