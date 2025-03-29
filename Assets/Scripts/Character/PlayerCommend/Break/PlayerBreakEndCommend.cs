public class PlayerBreakEndCommend : BasePlayerCommend
{
    InputActionTracker tracker;
    public PlayerBreakEndCommend(PlayerController playerController, InputActionTracker tracker) : base(playerController)
    {
        this.tracker = tracker;
    }

    public override void Execute()
    {
        if (!playerController.CanBeControlled)
        {
            return;
        }

        var args = new PlayerEvent.BeOrderToEndBreakTileEventArgs(tracker.ContinueScaledTime, tracker.StartScaledTime, tracker.EndScaledTime);
        playerController.BroadcastEvent(PlayerEvent.BeOrderToEndBreakTile, EEntityEventScope.Entity, args);
    }
}