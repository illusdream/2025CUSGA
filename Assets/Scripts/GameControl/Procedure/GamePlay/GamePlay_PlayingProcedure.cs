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