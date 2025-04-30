namespace Props
{
    public class YellowDuckPropState : DefaultPropState
    {
        public YellowDuckPropState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
        {
        }

        public override void OnUpdate()
        {
            var dir = PlayerController.playerInputHandler.Move.ActionValue;
            PlayerController.UpdatePlayerDirection(dir);
            if (EntityHandler.TryGetComponet(EntityComponetUsage.Moveable, out PlayerMoveComponent component))
            {
                component.Move(dir);
            }
            base.OnUpdate();
        }

        public override void OnExit()
        {
            PlayerController.CanSwitchPropUse = false;
            base.OnExit();
        }
    }
}