using System;
using ilsFramework;
using UnityEngine.InputSystem;

public class GamePlay_PlayingProcedure : ProcedureNode
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        var inputAction = InputManager.Instance.GetCurrentInputAction();
        inputAction.GamePlay.Pause.performed += Listener_PauseOnperformed;
        
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToPauseGame,Listener_OrderToPauseGame);
        
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        bool gameIsOver = false;
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            gameIsOver |= (!playerController?.IsAlive()).GetValueOrDefault(true);
        }

        if (gameIsOver)
        {
            ChangeState<GamePlay_EndProcedure>();
        }
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
        UIManager.Instance.GetUIPanel<InHouseUI>().Close();
        
        var inputAction = InputManager.Instance.GetCurrentInputAction();
        inputAction.GamePlay.Pause.performed -= Listener_PauseOnperformed;
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.OrderToPauseGame,Listener_OrderToPauseGame);
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Listener_PauseOnperformed(InputAction.CallbackContext obj)
    {
        //停止游戏
        ChangeState<GamePlay_PauseProcedure>();
    }

    private void Listener_OrderToPauseGame(EventArgs args)
    {
        //停止游戏
        ChangeState<GamePlay_PauseProcedure>();
    }
}