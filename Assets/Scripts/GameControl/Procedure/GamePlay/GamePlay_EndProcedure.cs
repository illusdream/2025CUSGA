using System;
using ilsFramework;
using UnityEngine;

public class GamePlay_EndProcedure : ProcedureNode
{
    private float oldtimeScale;
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        oldtimeScale = Time.timeScale;
        Time.timeScale = 0;

        var config = Config.GetConfig<GameControlConfig>();

        AudioManager.Instance.Play(AudioChannelName.Sound, config.GameOverSound);
        
        
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        
        //(switcher as GamePlayProcedure)?.ChangeState<StartMenuProcedure>();
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
        Time.timeScale = oldtimeScale;
        UIManager.Instance.GetUIPanel<InHouseUI>().Close();
        RandomEventManager.Instance.StopGameCommonRandomEventCycle();
        RandomEventManager.Instance.ClearAllRandomEvent();
        TileManager.Instance.StopFillRandomRange();
        VisualEffectManager.Instance.ClearAllVisualEffectPools();
        EntityManager.Instance.ClearAllEntities();
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }


}