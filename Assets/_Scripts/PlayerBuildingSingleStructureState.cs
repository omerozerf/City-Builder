using UnityEngine;

namespace _Scripts
{
    public class PlayerBuildingSingleStructureState : PlayerState
    {
        public PlayerBuildingSingleStructureState(GameManager gameManager) : base(gameManager)
        {
            
        }

        public override void OnInputPointerDown(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override void OnInputPointerChange(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override void OnInputPointerUp()
        {
            throw new System.NotImplementedException();
        }

        public override void OnInputPanChange(Vector3 panPosition)
        {
            throw new System.NotImplementedException();
        }

        public override void OnInputPanUp()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnCancel()
        {
            throw new System.NotImplementedException();
        }
    }
}