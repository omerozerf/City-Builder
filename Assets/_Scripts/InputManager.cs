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

        public event Action<Vector3> OnLeftMouseDown;
        public event Action OnLeftMouseUp;
        public event Action<Vector3> OnPointerChangeHandler; 
        public event Action<Vector3> OnRightMouseDown;
        public event Action OnRightMouseUp;

        private void Update()
        {
            CalculateMousePosition();
            CalculatePanningInput();
        }


        private void CallActionOnPointer(Action<Vector3> action)
        {
            var position = GetMousePosition();

            if (position.HasValue)
            {
                action.Invoke(position.Value);
                position = null;
            }
        }
        
        private void CalculateMousePosition()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                /*
                var position = GetMousePosition();

                if (position.HasValue)
                {
                    OnLeftMouseDown?.Invoke(position.Value);
                    position = null;
                }
                */
                CallActionOnPointer((position) => OnLeftMouseDown?.Invoke(position));
            }

            if (Input.GetMouseButton(0))
            {
                /*
                var position = GetMousePosition();

                if (position.HasValue)
                {
                    OnPointerChangeHandler?.Invoke(position.Value);
                    position = null;
                }
                */
                
                CallActionOnPointer((position) => OnPointerChangeHandler?.Invoke(position));
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnRightMouseUp?.Invoke();
            }
        }

        private Vector3? GetMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3? position = null;

            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity,
                    _mouseInputLayerMask))
            {
                position = hit.point - _groundTransform.position;
            }

            return position;
        }

        private void CalculatePanningInput()
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 position = Input.mousePosition;
                OnRightMouseDown?.Invoke(position);
            }

            if (Input.GetMouseButtonUp(1))
            {
                OnRightMouseUp?.Invoke();
            }
        }
    }
}
