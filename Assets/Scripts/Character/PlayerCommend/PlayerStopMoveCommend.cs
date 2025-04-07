public class PlayerStopMoveCommend : BasePlayerCommend
{
    public PlayerStopMoveCommend(PlayerController playerController) : base(playerController)
    {
    }

    public override void Execute()
    {
        if (!playerController.CanBeControlled)
        {
            return;
        }

        playerController?.ExecuteStopMoveCommand();
    }
}