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
        public event Action<Vector3> OnPointerDownHandler;
        public event Action OnPointerUpHandler;

        private void Update()
        {
            CalculatePointerPosition();
            CalculatePanningInput();
        }
        
        
        private void CalculatePointerPosition()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
                return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity,
                    _mouseInputLayerMask)) return;
             
            Vector3 position = hit.point - _groundTransform.position;
            OnInputCalculated?.Invoke(position);
        }

        private void CalculatePanningInput()
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 position = Input.mousePosition;
                OnPointerDownHandler?.Invoke(position);
            }

            if (Input.GetMouseButtonUp(1))
            {
                OnPointerUpHandler?.Invoke();
            }
        }
    }
}
