using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : ManagerSingleton<GameManager>,IManager
{
    [ShowInInspector]
    ProcedureSwitcher procedureSwitcher;
    
    GameControlConfig gameControlConfig;
    
    public bool GameProcedureEnabled { get; private set; }
    public void Init()
    {
        gameControlConfig = Config.GetConfig<GameControlConfig>();
        

        InitGameProcedureStateMachine();
    }

    public void Update()
    {
        if (GameProcedureEnabled)
        {
            procedureSwitcher.Update();
        }
    }

    public void LateUpdate()
    {
        if (GameProcedureEnabled)
        {
            procedureSwitcher.LateUpdate();
        }
    }

    public void FixedUpdate()
    {
        if (GameProcedureEnabled)
        {
            procedureSwitcher.FixedUpdate();
        }
    }

    public void OnDestroy()
    {
        procedureSwitcher.OnDestroy();
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        
    }

    public void InitGameProcedureStateMachine()
    {
        GameProcedureEnabled = gameControlConfig.EnableCommenProcedure;
        
        procedureSwitcher = new ProcedureSwitcher();
        
        procedureSwitcher.AddProcedureNode<StartMenuProcedure>();
        procedureSwitcher.AddProcedureNode<GamePlayProcedure>();
        if (GameProcedureEnabled)
        {
            procedureSwitcher.StartProcedure<StartMenuProcedure>();
        }
    }
    [Button]
    public void StartGameProcedureStateMachine()
    {
        if (!GameProcedureEnabled)
        {
            GameProcedureEnabled = true;
            procedureSwitcher.StartProcedure<StartMenuProcedure>();
        }
    }
    [Button]
    public void StopGameProcedureStateMachine()
    {
        if (GameProcedureEnabled)
        {
            GameProcedureEnabled = false;
        }
    }
}