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
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        bool gameIsOver = false;
        foreach (var playerController in CharacterManager.Instance.GetAllPlayers())
        {
            gameIsOver |= (!playerController.IsAlive());
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
}