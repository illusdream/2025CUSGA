using System;
using ilsFramework;

public class GamePlay_GuidelinesProcedure : SubProcedureSwitcher
{
    public override void OnInit()
    {
        AddProcedureNode<GamePlay_GuidelinesInitProcedure>();
        AddProcedureNode<GamePlay_PlayerObserveProcedure>();
        AddProcedureNode<GamePlay_PlayingProcedure>();
        AddProcedureNode<GamePlay_EndProcedure>();
        AddProcedureNode<GamePlay_PauseProcedure>();

        SetCurrentState<GamePlay_GuidelinesInitProcedure>();
        base.OnInit();
    }

    public override void OnEnter()
    {
        shili_InputManager.Instance.isGuide = true;
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToSwitchToMainMenu, Listener_OrderToSwitchToMainMenu);

        CharacterManager.Instance.EnablePlayRangeLimit();
        base.OnEnter();
        
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        shili_InputManager.Instance.isGuide = false;
        CharacterManager.Instance.DisablePlayRangeLimit();

        GlobalEventCenter.Instance?.RemoveListener(GlobalEventSets.OrderToSwitchToMainMenu, Listener_OrderToSwitchToMainMenu);

        SetCurrentState<GamePlay_GuidelinesInitProcedure>();
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

        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.GameRestart, EventArgs.Empty);

        ChangeProcedureNode<GamePlay_GuidelinesInitProcedure>();
        
        RandomEventManager.Instance.StopGameCommonRandomEventCycle();
        VisualEffectManager.Instance.ClearAllVisualEffectPools();
    }

    private void Listener_OrderToSwitchToMainMenu(EventArgs args)
    {
        TileManager.Instance.StopFillRandomRange();
        RandomEventManager.Instance.StopGameCommonRandomEventCycle();
        VisualEffectManager.Instance.ClearAllVisualEffectPools();
        
        ChangeState<StartMenuProcedure>();
    }
    private void Listener_OrderToGuidelinesScene(EventArgs args)
    {

    }
}