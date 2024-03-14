using Managers;
using UnityEngine;

namespace States
{
    public abstract class PlayerState
    {
        private GameManager m_GameManager;
        private CameraMovement m_CameraMovement;

        protected PlayerState(GameManager gameManager)
        {
            m_GameManager = gameManager;
            m_CameraMovement = gameManager.GetCameraMovement();
        }
        
        
        protected GameManager GetManager()
        {
            return m_GameManager;
        }

        
        public virtual void OnConfirmAction()
        {
            
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

        public virtual void EnterState(string structureName)
        {
            
        }
        
        public virtual void OnBuildArea(string structureName)
        {
            GetManager().TransitionToState(GetManager().GetBuildAreaState(), structureName);
        }

        public virtual void OnBuildSingleStructure(string structureName)
        {
            GetManager().TransitionToState(GetManager().GetBuildingSingleStructureState(), structureName);
        }

        public virtual void OnBuildRoad(string structureName)
        {
            GetManager().TransitionToState(GetManager().GetBuildingRoadState(), structureName);
        }

        public virtual void OnDemolishAction()
        {
            GetManager().TransitionToState(GetManager().GetRemoveBuildingState(), null);
        }
        
        public virtual void OnInputPanChange(Vector3 panPosition)
        {
            m_CameraMovement.MoveCamera(panPosition);
        }

        public virtual void OnInputPanUp()
        {
            m_CameraMovement.StopCameraMovement();
        }
        
        
        public abstract void OnCancel();
    }
}