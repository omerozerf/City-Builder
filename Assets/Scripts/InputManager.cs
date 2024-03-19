using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour, IInputManager
{
    private Action<Vector3> m_OnPointerSecondChangeHandler;
    private Action m_OnPointerSecondUpHandler;
    private Action<Vector3> m_OnPointerDownHandler;
    private Action m_OnPointerUpHandler;
    private Action<Vector3> m_OnPointerChangeHandler;

    [FormerlySerializedAs("mouseInputMask")] public LayerMask _mouseInputMask;

    public LayerMask MouseInputMask { get => _mouseInputMask; set => _mouseInputMask = value; }
    // Update is called once per frame
    void Update()
    {
        GetPointerPosition();
        GetPanningPointer();
    }


    private void GetPointerPosition()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CallActionOnPointer((position) => m_OnPointerDownHandler?.Invoke(position));

        }
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CallActionOnPointer((position) => m_OnPointerChangeHandler?.Invoke(position));
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_OnPointerUpHandler?.Invoke();
        }

    }

    private void CallActionOnPointer(Action<Vector3> action)
    {
        Vector3? position = GetMousePosition();
        if (position.HasValue)
        {
            action(position.Value);
            position = null;
        }
    }

    private Vector3? GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3? position = null;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, _mouseInputMask))
        {
            position = hit.point - transform.position;

        }

        return position;
    }

    private void GetPanningPointer()
    {
        if (Input.GetMouseButton(1))
        {
            var position = Input.mousePosition;
            m_OnPointerSecondChangeHandler?.Invoke(position);
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_OnPointerSecondUpHandler?.Invoke();
        }
    }

    public void AddListenerOnPointerDownEvent(Action<Vector3> listener)
    {
        m_OnPointerDownHandler += listener;
    }

    public void RemoveListenerOnPointerDownEvent(Action<Vector3> listener)
    {
        m_OnPointerDownHandler -= listener;
    }

    public void AddListenerOnPointerSecondDownEvent(Action<Vector3> listener)
    {
        m_OnPointerSecondChangeHandler += listener;
    }

    public void RemoveListenerOnPointerSecondChangeEvent(Action<Vector3> listener)
    {
        m_OnPointerSecondChangeHandler -= listener;
    }

    public void AddListenerOnPointerSecondUpEvent(Action listener)
    {
        m_OnPointerSecondUpHandler += listener;
    }

    public void RemoveListenerOnPointerSecondUpEvent(Action listener)
    {
        m_OnPointerSecondUpHandler -= listener;
    }

    public void AddListenerOnPointerUpEvent(Action listener)
    {
        m_OnPointerUpHandler += listener;
    }
    public void RemoveListenerOnPointerUpEvent(Action listener)
    {
        m_OnPointerUpHandler -= listener;
    }

    public void AddListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        m_OnPointerChangeHandler += listener;
    }

    public void RemoveListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        m_OnPointerChangeHandler -= listener;
    }
}
