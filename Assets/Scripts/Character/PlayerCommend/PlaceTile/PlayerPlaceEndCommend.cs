using System;

public class PlayerPlaceEndCommend: BasePlayerCommend
{
    public PlayerPlaceEndCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
        playerController.BroadcastEvent(PlayerEvent.BeOrderToEndPlaceTile,EEntityEventScope.Entity,EventArgs.Empty);
    }
}