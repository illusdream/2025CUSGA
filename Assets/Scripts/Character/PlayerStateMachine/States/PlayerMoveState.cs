using System;
using ilsFramework;
using UnityEngine.InputSystem;

public  class PlayerMoveState : BasePlayerState
{
    public PlayerMoveState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        //EntityHandler.AddEventListener(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,Listener_OrderToMove);
       // EntityHandler.AddEventListener(PlayerEvent.BeOrderToStartBreakTile,EEntityEventScope.Entity,Listener_BeOrderToStartBreakTile);
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        var dig = PlayerController.playerInputHandler.DigTile;
        if (dig.HasTriggered(0.3f) ||((InputAction)dig).IsPressed())
        {
            ChangeState<PlayerDigState>();
            dig.ResetTriggers();
            return;
        }
        
        var placeTile = PlayerController.playerInputHandler.PlaceTile;
        if (placeTile.HasTriggered(0.3f) || ((InputAction)placeTile).IsPressed())
        {
            ChangeState<PlayerPlaceTileState>();
            placeTile.ResetTriggers();
            return;
        }
        
        var useProp = PlayerController.playerInputHandler.UseProp;
        var colddownReady = (PlayerController.timerCollection[PlayerUsePropState.UsePropTimerName]?.IsFinish).GetValueOrDefault(false);
        if ((useProp.HasTriggered(0.3f) || ((InputAction)useProp).IsPressed()) && colddownReady)
        {
            ChangeState<PlayerUsePropState>();
            useProp.ResetTriggers();
            return;
        }
        
        var dir = PlayerController.playerInputHandler.Move.ActionValue;
        PlayerController.UpdatePlayerMoveAnimation(dir);
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
        //EntityHandler.RemoveEventListener(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,Listener_OrderToMove);
       // EntityHandler.RemoveEventListener(PlayerEvent.BeOrderToStartBreakTile,EEntityEventScope.Entity,Listener_BeOrderToStartBreakTile);
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    
}