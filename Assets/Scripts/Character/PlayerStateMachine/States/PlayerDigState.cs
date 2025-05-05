using System;
using ilsFramework;

public class PlayerDigState : BasePlayerState
{
    public PlayerDigState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
        
    }
    public override void OnEnter()
    {
        //播动画
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out BaseActionDirector actionDirector))
        {

            actionDirector.TryPlay(PlayerController.CurrenctDigAsset);
            actionDirector.onStopped += ActionDirectorOnonStopped;

        }
        base.OnEnter();
    }

    private void ActionDirectorOnonStopped(BaseActionDirector obj)
    {
        ChangeState<PlayerMoveState>();
    }

    public override void OnUpdate()
    {
        var dir = PlayerController.playerInputHandler.Move.ActionValue;
        PlayerController.UpdatePlayerDirection(dir);
        if (EntityHandler.TryGetComponet(EntityComponetUsage.Moveable, out PlayerMoveComponent component))
        {
            component.Move(dir);
        }
        
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out BaseActionDirector actionDirector))
        {
            if (actionDirector.ControlTrackHandler.clipType == EControlClipType.LoopByCondition)
            {
                var Digging = PlayerController.playerInputHandler.DigTile._trackedAction.IsPressed();
                actionDirector.ControlTrackHandler.SetLoop(Digging);
            }
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
}