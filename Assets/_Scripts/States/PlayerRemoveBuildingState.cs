using UnityEngine;

namespace States
{
    public class PlayerRemoveBuildingState : PlayerState
    {
        public PlayerRemoveBuildingState(GameManager gameManager) : base(gameManager)
        {
            
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            
        }

        public override void OnInputPointerChange(Vector3 position)
        {
            
        }

        public override void OnInputPointerUp()
        {
            
        }

        public override void OnInputPanChange(Vector3 panPosition)
        {
            
        }

        public override void OnInputPanUp()
        {
            
        }

        public override void OnCancel()
        {
            GetManager().TransitionToState(GetManager().GetSelectionState());
        }
    }
}