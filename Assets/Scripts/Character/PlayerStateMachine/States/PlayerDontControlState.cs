using UnityEngine;

public class PlayerDontControlState : BasePlayerState
{
    public PlayerDontControlState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
    }

    public override void OnUpdate()
    {
        if (PlayerController.CanBeControlled)
        {
            ChangeState<PlayerMoveState>();
        }
        base.OnUpdate();
    }
}