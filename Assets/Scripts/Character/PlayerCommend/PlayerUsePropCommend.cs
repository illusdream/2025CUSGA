using System;

public class PlayerUsePropCommend : BasePlayerCommend
{
    public PlayerUsePropCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
        playerController.BroadcastEvent(PlayerEvent.BeOrderToUseProp,EEntityEventScope.Component,EventArgs.Empty);
    }
}