using System;

public class PlayerBreakStartCommend : BasePlayerCommend
{
    public PlayerBreakStartCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
        if (!playerController.CanBeControlled)
        {
            return;
        }
        playerController.BroadcastEvent(PlayerEvent.BeOrderToStartBreakTile,EEntityEventScope.Entity,EventArgs.Empty);
    }
}