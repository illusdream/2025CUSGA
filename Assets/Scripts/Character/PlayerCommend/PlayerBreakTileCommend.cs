using System;

public class PlayerBreakTileCommend : BasePlayerCommend
{
    public PlayerBreakTileCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
       playerController.BroadcastEvent(PlayerEvent.BeOrderToBreakTile,EEntityEventScope.Component,EventArgs.Empty);
    }
}