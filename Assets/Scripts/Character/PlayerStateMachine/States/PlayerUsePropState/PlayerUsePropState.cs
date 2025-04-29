using System;
using DefaultNamespace;
using ilsFramework;
using UnityEngine.Timeline;

public class PlayerUsePropState : BasePlayerState
{
    public const string UsePropTimerName = "UsePropTimer";
    
    public PropStateHandler propHandler;
    
    private BasePropState propState;

    private BaseProp NowUsingProp;
    public PlayerUsePropState(EntityHandler handler, PlayerController playerController) : base(handler, playerController)
    {
        propHandler = new PropStateHandler();
        propHandler.onOrderToChangePlayerState += ChangeStateByHandler;
        propHandler.onOrderToPlayTimelineAsset += PlayerPropOrderedTimeline;
        propHandler.onRemoveProp += RemoveTargetProp;
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
            var result = propContainer.GetLastProp();
            if (result is null)
            {
                ChangeState<PlayerMoveState>();
                return;
            }
            
            //获取对应状态机
            NowUsingProp = result;
            var stateInstance = Activator.CreateInstance(result.PropStateType,new object[]{EntityHandler,PlayerController}) as BasePropState;
            propState = stateInstance;
            if (propState is null)
            {
                return;
            }
            propState.PropStateHandler = propHandler;
            propState.Prop = NowUsingProp;
            propState.OnInit();
            propState?.OnEnter();
            
            NowUsingProp.SetInputHandler(PlayerController.playerInputHandler);


            PlayerController.timerCollection.CreateTimer(1, 1, UsePropTimerName).Register();
        }
        base.OnEnter();
    }


    public override void OnUpdate()
    {
        //ChangeState<PlayerMoveState>();
        propState?.OnUpdate();
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        propState?.OnFixedUpdate();
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        propState?.OnExit();
        base.OnExit();
    }

    public override void OnDestroy()
    {
        propState?.OnDestroy();
        base.OnDestroy();
    }

    private void ChangeStateByHandler(Type stateType)
    {
        ChangeState(stateType);
    }
    
    private void PlayerPropOrderedTimeline(TimelineAsset timelineAsset)
    {
        if (EntityHandler.TryGetComponet(EntityComponetUsage.ActionDirector, out PlayerActionDirector actionDirector))
        {
            foreach (var trackAsset in timelineAsset.GetOutputTracks())
            {
                if (trackAsset is PropUsingTrack propUsingTrack)
                {
                    foreach (var clip in propUsingTrack.GetClips())
                    {
                        ((PropUsingClip)clip.asset).SetClipProperty(NowUsingProp, EntityHandler);
                    }
                }
            }
            actionDirector.TryPlay(timelineAsset);
        }
    }

    private void RemoveTargetProp(BaseProp prop)
    {
        if (EntityHandler.TryGetComponet(EntityComponetUsage.PropContainer,out PlayerPropContainer propContainer))
        {
            propContainer.RemoveProp(prop);
        }
    }
}