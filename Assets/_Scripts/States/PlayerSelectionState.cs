using Managers;
using UnityEngine;

namespace States
{
    public class PlayerSelectionState : PlayerState
    {
        public PlayerSelectionState(GameManager gameManager) : base(gameManager)
        {
            
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
        
        public override void OnCancel()
        {
            return;
        }
    }
}