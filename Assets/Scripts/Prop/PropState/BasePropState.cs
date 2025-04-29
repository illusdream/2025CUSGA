namespace PropState
{
    public class BasePropState : BasePlayerState
    {
        public BaseProp Prop { get; set; }
        
        public BasePropState(EntityHandler handler, PlayerController playerController,BaseProp prop) : base(handler, playerController)
        {
        }
    }
}