using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _mouseInputLayerMask;
        
        
        private void Update()
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            GetInput();
        }
        
        
        private void GetInput()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
                return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity,
                    _mouseInputLayerMask)) return;
            
            Vector3 position = hit.point - transform.position;
            Debug.Log("Mouse position: " + position);
        }
    }
}
