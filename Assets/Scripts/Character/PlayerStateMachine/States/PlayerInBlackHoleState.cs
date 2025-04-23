using System;
using ilsFramework;

public class PlayerInBlackHoleState : BasePlayerState
{
    public PlayerInBlackHoleState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
        
    }
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {    
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out BaseActionDirector actionDirector))
        {
            actionDirector.TryPlay(PlayerController.IntoBlackHoleTimelineAsset);
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
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out BaseActionDirector actionDirector) && EntityHandler.TryGetComponet(EntityComponetUsage.Buff, out BaseBuffContainer buffContainer))
        {
            if (actionDirector.ControlTrackHandler.clipType == EControlClipType.LoopByCondition)
            {
                var loop = buffContainer.HasBuff(EBuffType.InBlackHoleBuff);
                actionDirector.ControlTrackHandler.SetLoop(loop);
            }
        }
        base.OnUpdate();
    }

    private void ExitBlackHole(BaseActionDirector obj)
    {
        ChangeState<PlayerMoveState>();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
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

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}