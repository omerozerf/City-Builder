using UnityEngine;

namespace _Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Vector3? m_BasePointerPosition = null;
        private float m_XMin;
        private float m_XMax;
        private float m_ZMin;
        private float m_ZMax;

        
        private void LimitPositionInsideCameraBounds()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, m_XMin, m_XMax),
                0,
                Mathf.Clamp(transform.position.z, m_ZMin, m_ZMax));
        }

        public void SetCameraLimits(int xMin, int xMax, int zMin, int zMax)
        {
            m_XMin = xMin;
            m_XMax = xMax;
            m_ZMin = zMin;
            m_ZMax = zMax;
        }
        
        public void StopCameraMovement()
        { 
            m_BasePointerPosition = null;
        }
        
        public void MoveCamera(Vector3 pointerPosition)
        {
            if (!m_BasePointerPosition.HasValue)
            { 
                m_BasePointerPosition = pointerPosition;
            }
            
            Vector3 newPosition = pointerPosition - m_BasePointerPosition.Value;
            newPosition = new Vector3(newPosition.x, 0, newPosition.y);
            
            transform.Translate(newPosition * (_speed * Time.deltaTime));
            
            LimitPositionInsideCameraBounds();
        }
    }
}