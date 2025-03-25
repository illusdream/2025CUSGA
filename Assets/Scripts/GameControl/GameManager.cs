using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : ManagerSingleton<GameManager>,IManager
{
    [ShowInInspector]
    ProcedureSwitcher procedureSwitcher;
    public void Init()
    {
        procedureSwitcher = new ProcedureSwitcher();
        
        procedureSwitcher.AddProcedureNode<StartMenuProcedure>();
        procedureSwitcher.AddProcedureNode<GamePlayProcedure>();
        procedureSwitcher.StartProcedure<StartMenuProcedure>();
        
    }

    public void Update()
    {
        procedureSwitcher.Update();
    }

    public void LateUpdate()
    {
        procedureSwitcher.LateUpdate();
    }

    public void FixedUpdate()
    {
        procedureSwitcher.FixedUpdate();
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
}