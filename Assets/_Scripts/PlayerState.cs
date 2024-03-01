using UnityEngine;

namespace _Scripts
{
    public abstract class PlayerState
    {
        private GameManager m_GameManager;
        
        
        public PlayerState(GameManager gameManager)
        {
            m_GameManager = gameManager;
        }
        
        
        protected GameManager GetManager()
        {
            return m_GameManager;
        }
        
        
        public abstract void OnInputPointerDown(Vector3 position);
        public abstract void OnInputPointerChange(Vector3 position);
        public abstract void OnInputPointerUp();
        public abstract void OnInputPanChange(Vector3 panPosition);
        public abstract void OnInputPanUp();

        public virtual void EnterState()
        {
            
        }

        public abstract void OnCancel();
    }
}