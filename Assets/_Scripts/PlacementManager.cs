using UnityEngine;

namespace _Scripts
{
    public class PlacementManager : MonoBehaviour
    { 
        [SerializeField] private GameObject _buildPrefab;
        [SerializeField] private Transform _groundTransform;


        public void Build(Vector3 position, GridStructure gridStructure)
        {
            Vector3 fixedPosition = position + _groundTransform.position;
            GameObject newStructure = Instantiate(_buildPrefab, fixedPosition, Quaternion.identity);
            
            gridStructure.PlaceStructureOnTheGrid(newStructure, fixedPosition);
        }
    }
}
