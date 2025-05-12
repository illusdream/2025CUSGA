using ilsFramework;

public class PlayerPlaceTileState : BasePlayerState
{
    public PlayerPlaceTileState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
    }

    public override void OnInit()
    {

        base.OnInit();
    }

    public override void OnEnter()
    {     
        //播动画
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out BaseActionDirector actionDirector))
        {

            actionDirector.TryPlay(PlayerController.PlaceTileAsset);
            actionDirector.onStopped += ActionDirectorOnonStopped;

        }
        base.OnEnter();
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

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        //播动画
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out BaseActionDirector actionDirector))
        {
            actionDirector.onStopped -= ActionDirectorOnonStopped;
        }
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void ActionDirectorOnonStopped(BaseActionDirector obj)
    {
        ChangeState<PlayerMoveState>();
    }
}