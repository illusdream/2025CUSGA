namespace Props
{
    public class MortarPropState : DefaultPropState
    {
        public MortarPropState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
        {
        }

        public override void OnExit()
        {
            PlayerController.CanSwitchPropUse = false;
            PlayerController.SetCanMove(false);
            PlayerController.CanUpdatePlayerDirection = false;
            base.OnExit();
        }
    }
}