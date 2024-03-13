using Managers;

namespace States
{
    public class PlayerBuildingRoadState : PlayerState
    {
        public PlayerBuildingRoadState(GameManager gameManager) : base(gameManager)
        {
        }

        public override void OnCancel()
        {
            
        }
    }
}