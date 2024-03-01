using UnityEngine;

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
    
    public void RemoveBuilding(Vector3 position, GridStructure gridStructure)
    {
        //TODO .
        /*
        Vector3 fixedPosition = position + _groundTransform.position;
        gridStructure.RemoveStructureFromTheGrid(fixedPosition);
        */
    }
}