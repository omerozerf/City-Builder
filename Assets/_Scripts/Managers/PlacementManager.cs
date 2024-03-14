using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlacementManager : MonoBehaviour
    { 
        [SerializeField] private Transform _groundTransform;
        [SerializeField] private Material _transparentMaterial;
        
        private Dictionary<GameObject, Material[]> m_OriginalMaterialsMap = new ();
        
        
        /*
        public void Build(Vector3 position, GridStructure gridStructure, GameObject buildPrefab)
        {
            Vector3 fixedPosition = position + _groundTransform.position;
            
            GameObject newStructure = Instantiate(buildPrefab, fixedPosition, Quaternion.identity);
            
            gridStructure.PlaceStructureOnTheGrid(newStructure, fixedPosition);
        }
        */

        public GameObject CreateGhostStructure(Vector3 gridPos, GameObject buildingPrefab)
        {
            Vector3 fixedPosition = gridPos + _groundTransform.position;
            GameObject newStructure = Instantiate(buildingPrefab, fixedPosition, Quaternion.identity);

            foreach (Transform child in newStructure.transform)
            {
                MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
                if (!m_OriginalMaterialsMap.ContainsKey(child.gameObject))
                {
                    m_OriginalMaterialsMap.Add(child.gameObject, meshRenderer.materials);
                }
                
                Material[] materialToSet = new Material[meshRenderer.materials.Length];
                for (int i = 0; i < materialToSet.Length; i++)
                {
                    materialToSet[i] = _transparentMaterial;
                    materialToSet[i].color = Color.green;
                }
                
                meshRenderer.materials = materialToSet;
            }

            return newStructure;
        }

        public void ConfirmPlacement(IEnumerable<GameObject> structureCollection)
        {
            foreach (GameObject structure in structureCollection)
            {
                foreach (Transform childTransform in structure.transform)
                {
                    MeshRenderer meshRenderer = childTransform.GetComponent<MeshRenderer>();

                    if (m_OriginalMaterialsMap.ContainsKey(childTransform.gameObject))
                    {
                        meshRenderer.materials = m_OriginalMaterialsMap[childTransform.gameObject];
                    }
                }
            }
            
            m_OriginalMaterialsMap.Clear();
        }

        public void CancelPlacement(IEnumerable<GameObject> structureCollection)
        {
            foreach (GameObject structure in structureCollection)
            {
                Destroy(structure);
            }
            
            m_OriginalMaterialsMap.Clear();
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