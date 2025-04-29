public class DefaultPropState : BasePropState
{
    public DefaultPropState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        if (EntityHandler.TryGetComponet(EntityComponetUsage.PropContainer, out PlayerPropContainer propContainer) &&
            EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector, out PlayerActionDirector actionDirector))
        {
            
            PropStateHandler.PlayTimelineAsset(Prop.GetPlayTimelineAsset(PlayerController));
            actionDirector.onStopped += ActionDirectorOnonStopped;
        }

        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector, out PlayerActionDirector actionDirector))
        {
            actionDirector.onStopped -= ActionDirectorOnonStopped;
        }
        base.OnExit();
    }

    public override void OnDestroy()
    {
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector, out PlayerActionDirector actionDirector))
        {
            actionDirector.onStopped -= ActionDirectorOnonStopped;
        }
        base.OnDestroy();
    }


    private void ActionDirectorOnonStopped(BaseActionDirector obj)
    {
        PropStateHandler.RemoveThisProp(Prop);
        PropStateHandler.ChangePlayerState<PlayerMoveState>();
    }
}