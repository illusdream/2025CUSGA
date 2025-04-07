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
        UIManager.Instance.GetUIPanel<InHouseUI>().Open();
        
        var inputAction = InputManager.Instance.GetCurrentInputAction();
        inputAction.GamePlay.Pause.performed += Listener_PauseOnperformed;
        
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.OrderToPauseGame,Listener_OrderToPauseGame);
        
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        bool gameIsOver = false;
        bool isPlayer1Win = false;
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            gameIsOver |= (!playerController?.IsAlive()).GetValueOrDefault(true);
            if (playerController.PlayerID == 2 && gameIsOver)
            {
                isPlayer1Win = true;
            }
        }

        GlobalEventSets.GameOverEventArgs args = null;

        CharacterManager.Instance.TryGetPlayerController(1, out var player1);
        CharacterManager.Instance.TryGetPlayerController(2, out var player2);

        if (isPlayer1Win)
        {
            args = new GlobalEventSets.GameOverEventArgs(player1, player2);
            
        }
        else
        {
            args = new GlobalEventSets.GameOverEventArgs(player2, player1);
            
        }


        

        
        if (gameIsOver)
        {
            GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.GameOver, args);
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