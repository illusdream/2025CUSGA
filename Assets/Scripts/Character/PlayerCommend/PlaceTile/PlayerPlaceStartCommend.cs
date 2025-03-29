using System;

public class PlayerPlaceStartCommend: BasePlayerCommend
{
    public PlayerPlaceStartCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
        if (!playerController.CanBeControlled)
        {
            return;
        }
        playerController.BroadcastEvent(PlayerEvent.BeOrderToStartPlaceTile,EEntityEventScope.Entity,EventArgs.Empty);
    }
}