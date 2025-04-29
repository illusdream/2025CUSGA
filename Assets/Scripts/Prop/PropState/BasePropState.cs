using ilsFramework;

public class BasePropState : BasePlayerState
{
    public BaseProp Prop { get; set; }
        
    public PropStateHandler PropStateHandler { get; set; }
    
    public BasePropState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
    }
}