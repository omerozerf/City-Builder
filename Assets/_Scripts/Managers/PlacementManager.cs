using UnityEngine;

namespace Managers
{
    public class PlacementManager : MonoBehaviour
    { 
        [SerializeField] private Transform _groundTransform;


        public void Build(Vector3 position, GridStructure gridStructure, GameObject buildPrefab)
        {
            Vector3 fixedPosition = position + _groundTransform.position;
            GameObject newStructure = Instantiate(buildPrefab, fixedPosition, Quaternion.identity);
            
            gridStructure.PlaceStructureOnTheGrid(newStructure, fixedPosition);
        }
    
        public void RemoveBuilding(Vector3 position, GridStructure gridStructure)
        {
            /*
        Vector3 fixedPosition = position + _groundTransform.position;
        gridStructure.RemoveStructureFromTheGrid(fixedPosition);
        */
        
            GameObject structure = gridStructure.GetStructureFromTheGrid(position);

            if (!structure) return;
        
            Destroy(structure);
            gridStructure.RemoveStructureFromTheGrid(position);
        }
    }
}