using System;
using ilsFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlay_PauseProcedure : ProcedureNode
{
    private float oldTimeScale;
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        var inputAction = InputManager.Instance.GetCurrentInputAction();
        inputAction.GamePlay.Pause.performed += Listener_PauseOnperformed;
        
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToResumeGame,Listener_OrderToResumeGame);
        
        UIManager.Instance.GetUIPanel<StopGameUI>().Open();
        oldTimeScale = Time.timeScale;
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        Time.timeScale = 0;
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
        var inputAction = InputManager.Instance.GetCurrentInputAction();
        inputAction.GamePlay.Pause.performed -= Listener_PauseOnperformed;
        
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.OrderToResumeGame,Listener_OrderToResumeGame);
        
        Time.timeScale = oldTimeScale;
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Listener_PauseOnperformed(InputAction.CallbackContext obj)
    {
        ChangeStateByPopStack();
    }

    private void Listener_OrderToResumeGame(EventArgs args)
    {
        ChangeStateByPopStack();
    }
}