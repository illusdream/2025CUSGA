using System;

public class PlayerPlaceTileCommend : BasePlayerCommend
{
    public PlayerPlaceTileCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
        playerController.BroadcastEvent(PlayerEvent.BeOrderToPlaceTile,EEntityEventScope.Component,EventArgs.Empty);
    }
}