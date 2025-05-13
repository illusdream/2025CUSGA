using System;
using System.Linq;
using ilsFramework;

public class GamePlayProcedure : SubProcedureSwitcher
{
    
    AudioEmitter emitter;
    
    public override void OnInit()
    {

        AddProcedureNode<GamePlay_InitProcedure>();
        AddProcedureNode<GamePlay_PlayerObserveProcedure>();
        AddProcedureNode<GamePlay_PlayingProcedure>();
        AddProcedureNode<GamePlay_EndProcedure>();
        AddProcedureNode<GamePlay_PauseProcedure>();

        SetCurrentState<GamePlay_InitProcedure>();
        base.OnInit();
    }

    public override void OnEnter()
    {
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToRestartGamePlay,Listener_OrderToRestartGamePlay);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToSwitchToMainMenu,Listener_OrderToSwitchToMainMenu);
        
        CharacterManager.Instance.EnablePlayRangeLimit();

        emitter = AudioManager.Instance.Play(AudioChannelName.BGM, GetRandomTargetAudio());
        emitter.OnStop += OnStop;
        
        base.OnEnter();
    }

    private void OnStop()
    {
        emitter = AudioManager.Instance.Play(AudioChannelName.BGM, GetRandomTargetAudio());
        emitter.OnStop += OnStop;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        CharacterManager.Instance.DisablePlayRangeLimit();
        
        GlobalEventCenter.Instance?.RemoveListener(GlobalEventSets.OrderToRestartGamePlay,Listener_OrderToRestartGamePlay);
        GlobalEventCenter.Instance?.RemoveListener(GlobalEventSets.OrderToSwitchToMainMenu,Listener_OrderToSwitchToMainMenu);
        
        SetCurrentState<GamePlay_InitProcedure>();
        
        emitter?.Stop();
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void Listener_OrderToRestartGamePlay(EventArgs args)
    {
        TileManager.Instance.StopFillRandomRange();
        
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.GameRestart,EventArgs.Empty);
        
        RandomEventManager.Instance.StopGameCommonRandomEventCycle();
        RandomEventManager.Instance.ClearAllRandomEvent();
        VisualEffectManager.Instance.ClearAllVisualEffectPools();
        EntityManager.Instance.ClearAllEntities();
        ChangeProcedureNode<GamePlay_InitProcedure>();
    }

    private void Listener_OrderToSwitchToMainMenu(EventArgs args)
    {
        TileManager.Instance.StopFillRandomRange();
        
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.GameRestart,EventArgs.Empty);
        
        RandomEventManager.Instance.StopGameCommonRandomEventCycle();
        VisualEffectManager.Instance.ClearAllVisualEffectPools();
        RandomEventManager.Instance.ClearAllRandomEvent();
        EntityManager.Instance.ClearAllEntities();
        ChangeState<StartMenuProcedure>();
    }


    public SoundData GetRandomTargetAudio()
    {
        var config = Config.GetConfig<GameControlConfig>();
        var result = config.FightSounds.Shuffle();
        return result.First();
    }
}