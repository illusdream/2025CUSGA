using System;
using ilsFramework;

public class GamePlayProcedure : SubProcedureSwitcher
{
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
        base.OnEnter();
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
        
        ChangeProcedureNode<GamePlay_InitProcedure>();
    }

    private void Listener_OrderToSwitchToMainMenu(EventArgs args)
    {
        
        ChangeState<StartMenuProcedure>();
    }
}