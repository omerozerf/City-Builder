using System;
using UnityEngine;
using UnityEngine.EventSystems;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace _Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _mouseInputLayerMask;
        [SerializeField] private Transform _groundTransform;

        public event Action<Vector3> OnInputCalculated;
        

        private void Update()
        {
            CalculateInput();
        }
        
        
        private void CalculateInput()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
                return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity,
                    _mouseInputLayerMask)) return;
             
            Vector3 position = hit.point - _groundTransform.position;
            OnInputCalculated?.Invoke(position);
        }
    }
}
