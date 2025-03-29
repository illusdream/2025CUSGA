using System;
using Cysharp.Threading.Tasks;
using ilsFramework;
using UnityEngine;

public class GamePlay_PlayerObserveProcedure : ProcedureNode
{
    private float timescale;
    public override void OnInit()
    {
        base.OnInit();
    }

    public async override void OnEnter()
    {
        var config = Config.GetConfig<GameControlConfig>();
        
        UIManager.Instance.GetUIPanel<OnOpenGameUI>().Open();
        
        await UniTask.Delay(TimeSpan.FromSeconds(config.ObservePlayerTimeWhenStarted),DelayType.Realtime);
        
        CharacterManager.Instance.SetAllPlayerCanBeControlled(true);
        ChangeState<GamePlay_PlayingProcedure>();
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {        
        base.OnExit();
    }
}