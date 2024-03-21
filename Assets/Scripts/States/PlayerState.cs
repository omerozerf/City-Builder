using UnityEngine;

namespace States
{
    public abstract class PlayerState
    {
        protected GameManager gameManager;
        protected CameraMovement cameraMovement;
        public PlayerState(GameManager gameManager)
        {
            this.gameManager = gameManager;
            cameraMovement = gameManager._cameraMovement;
        }
        public virtual void OnConfirmAction()
        {
            gameManager.TransitionToState(gameManager.selectionState,null);
        }
        public virtual void OnInputPointerDown(Vector3 position) { }
        public virtual void OnInputPointerChange(Vector3 position) { }
        public virtual void OnInputPointerUp() { }
        public virtual void OnInputPanChange(Vector3 panPosition)
        {
            cameraMovement.MoveCamera(panPosition);
        }

        public virtual void OnInputPanUp()
        {
            cameraMovement.StopCameraMovement();
        }
        public virtual void EnterState(string variable)
        {

        }
        public virtual void OnBuildArea(string structureName)
        {
            gameManager.TransitionToState(gameManager.buildingAreaState, structureName);
        }

        public virtual void OnBuildSingleStructure(string structureName)
        {
            gameManager.TransitionToState(gameManager.buildingSingleStructureState, structureName);
        }

        public virtual void OnBuildRoad(string structureName)
        {
            gameManager.TransitionToState(gameManager.buildingRoadState, structureName);
        }

        public virtual void OnDemolishAction()
        {
            gameManager.TransitionToState(gameManager.demolishState, null);
        }

        public abstract void OnCancle();

    }
}
