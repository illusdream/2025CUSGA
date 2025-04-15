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
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        if (EntityHandler.TryGetComponet(EntityComponetUsage.playerTileHandler, out PlayerTileHandler playerTileHandler))
        {
            playerTileHandler.TryPlaceTile();
        }
        var dir = PlayerController.playerInputHandler.Move.ActionValue;
        PlayerController.UpdatePlayerMoveAnimation(dir);
        PlayerController.UpdatePlayerDirection(dir);
        if (EntityHandler.TryGetComponet(EntityComponetUsage.Moveable, out PlayerMoveComponent component))
        {
            component.Move(dir);
        }
        ChangeState<PlayerMoveState>();
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}