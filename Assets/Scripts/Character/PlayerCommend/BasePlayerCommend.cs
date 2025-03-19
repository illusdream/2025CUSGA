public abstract class BasePlayerCommend : IPlayerCommend
{
    public PlayerController playerController;

    public BasePlayerCommend(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    
    public abstract void Execute();
}