using Managers;
using UnityEngine;

namespace States
{
    public abstract class PlayerState
    {
        private GameManager m_GameManager;


        protected PlayerState(GameManager gameManager)
        {
            m_GameManager = gameManager;
        }
        
        
        protected GameManager GetManager()
        {
            return m_GameManager;
        }


        public virtual void OnInputPointerDown(Vector3 position)
        {
            
        }
        
        public virtual void OnInputPointerChange(Vector3 position)
        {
            
        }

        public virtual void OnInputPointerUp()
        {
            
        }

        public virtual void OnInputPanChange(Vector3 panPosition)
        {
            
        }

        public virtual void OnInputPanUp()
        {
            
        }

        public virtual void EnterState(string structureName)
        {
            
        }

        public virtual void OnBuildArea(string structureName)
        {
            
        }
        
        public virtual void OnBuildSingleStructure(string structureName)
        {
            
        }
        
        public virtual void OnBuildRoad(string structureName)
        {
            
        }
        
        public virtual void OnDemolishAction()
        {
            
        }
        

        public abstract void OnCancel();
    }
}