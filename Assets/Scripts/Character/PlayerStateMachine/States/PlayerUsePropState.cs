using DefaultNamespace;
using ilsFramework;
using UnityEngine.Timeline;

public class PlayerUsePropState : BasePlayerState
{
    public const string UsePropTimerName = "UsePropTimer";
    public PlayerUsePropState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
    }

    public override void OnInit()
    {
        PlayerController.timerCollection.CreateTimer(0.01f, 1, UsePropTimerName).Register();
        base.OnInit();
    }

    public override void OnEnter()
    {
        //获取当前Prop
        if (EntityHandler.TryGetComponet(EntityComponetUsage.PropContainer,out PlayerPropContainer propContainer)&&
            EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector,out PlayerActionDirector actionDirector))
        {
            var result = propContainer.PopLastProp();
            if (result is null)
            {
                ChangeState<PlayerMoveState>();
                return;
            }
            result.SetInputHandler(PlayerController.playerInputHandler);
            var timelineAsset = result.GetPlayTimelineAsset(PlayerController);
            foreach (var trackAsset in  timelineAsset.GetOutputTracks())
            {
                if (trackAsset is PropUsingTrack propUsingTrack)
                {
                    foreach (var clip in propUsingTrack.GetClips())
                    {
                        ((PropUsingClip)clip.asset).SetClipProperty(result,EntityHandler);
                    }
                }
            }
            actionDirector.TryPlay(timelineAsset);
            actionDirector.onStopped += ActionDirectorOnonStopped;
            PlayerController.timerCollection.CreateTimer(1, 1, UsePropTimerName).Register();
        }
        base.OnEnter();
    }

    private void ActionDirectorOnonStopped(BaseActionDirector obj)
    {
        ChangeState<PlayerMoveState>();
    }

    public override void OnUpdate()
    {
        //ChangeState<PlayerMoveState>();
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
}