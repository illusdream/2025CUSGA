public class GamePlay_InitProcedure : ProcedureNode
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        TileManager.Instance.GenerateTiles();
        CharacterManager.Instance.InitAllPlayers();
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        ChangeState<GamePlay_PlayingProcedure>();
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
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}