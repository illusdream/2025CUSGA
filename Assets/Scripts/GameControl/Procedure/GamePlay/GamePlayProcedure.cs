public class GamePlayProcedure : SubProcedureSwitcher
{
    public override void OnInit()
    {
        AddProcedureNode<GamePlay_InitProcedure>();
        AddProcedureNode<GamePlay_PlayingProcedure>();
        AddProcedureNode<GamePlay_EndProcedure>();
        AddProcedureNode<GamePlay_PauseProcedure>();
        
        SetCurrentState<GamePlay_InitProcedure>();
        base.OnInit();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
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
}