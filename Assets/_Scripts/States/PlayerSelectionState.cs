using Managers;
using UnityEngine;

namespace States
{
    public class PlayerSelectionState : PlayerState
    {
        private CameraMovement m_CameraMovement;
        
        public PlayerSelectionState(GameManager gameManager, CameraMovement cameraMovement) : base(gameManager)
        {
            m_CameraMovement = cameraMovement;
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            return;
        }

        public override void OnInputPointerChange(Vector3 position)
        {
            return;
        }

        public override void OnInputPointerUp()
        {
            return;
        }

        public override void OnInputPanChange(Vector3 panPosition)
        {
            m_CameraMovement.MoveCamera(panPosition);
        }

        public override void OnInputPanUp()
        {
            m_CameraMovement.StopCameraMovement();
        }

        public override void OnBuildArea(string structureName)
        {
            base.OnBuildArea(structureName);
            
            // GetManager().TransitionToState(GetManager().GetBuildingSingleStructureState(), structureName);
        }

        public override void OnBuildSingleStructure(string structureName)
        {
            base.OnBuildSingleStructure(structureName);
            
            GetManager().TransitionToState(GetManager().GetBuildingSingleStructureState(), structureName);
        }

        public override void OnBuildRoad(string structureName)
        {
            base.OnBuildRoad(structureName);
            
            // GetManager().TransitionToState(GetManager().GetBuildingSingleStructureState(), structureName);
        }

        public override void OnCancel()
        {
            return;
        }
    }
}