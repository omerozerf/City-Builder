using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _mouseInputLayerMask;
        [SerializeField] private GameObject _buildPrefab;

        private const int CELL_SIZE = 2;

        private void Update()
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            GetInput();
        }
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void GetInput()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
                return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity,
                    _mouseInputLayerMask)) return;
            
            Vector3 position = hit.point - transform.position;
            Debug.Log("Mouse position: " + CalculateGridPosition(position));
            
            Build(CalculateGridPosition(position));
        }
        
        private Vector3 CalculateGridPosition(Vector3 inputPosition)
        {
            int x = Mathf.FloorToInt((float) inputPosition.x / CELL_SIZE);
            int z = Mathf.FloorToInt((float) inputPosition.z / CELL_SIZE);
            
            return new Vector3(x * CELL_SIZE, 0, z * CELL_SIZE);
        }
        
        private void Build(Vector3 position)
        {
            Instantiate(_buildPrefab, position, Quaternion.identity);
        }
    }
}
