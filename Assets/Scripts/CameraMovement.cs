using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    Vector3? m_BasePointerPosition = null;
    [FormerlySerializedAs("cameraMovementSpeed")] public float _cameraMovementSpeed = 0.05f;
    private int m_CameraXMin, m_CameraXMax, m_CameraZMin, m_CameraZMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(Vector3 pointerposition)
    {
        if (m_BasePointerPosition.HasValue == false)
        {
            m_BasePointerPosition = pointerposition;
        }
        Vector3 newPosition = pointerposition - m_BasePointerPosition.Value;
        newPosition = new Vector3(newPosition.x, 0, newPosition.y);
        transform.Translate(newPosition * _cameraMovementSpeed);
        LimitPositionInsideCameraBounds();
    }

    private void LimitPositionInsideCameraBounds()
    {
        transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, m_CameraXMin, m_CameraXMax),
                    0,
                    Mathf.Clamp(transform.position.z, m_CameraZMin, m_CameraZMax)
                    );
    }

    public void StopCameraMovement()
    {
        m_BasePointerPosition = null;
    }

    public void SetCameraLimits(int cameraXMin, int cameraXMax, int cameraZMin, int cameraZMax)
    {
        m_CameraXMax = cameraXMax;
        m_CameraXMin = cameraXMin;
        m_CameraZMax = cameraZMax;
        m_CameraZMin = cameraZMin;
    }
}
