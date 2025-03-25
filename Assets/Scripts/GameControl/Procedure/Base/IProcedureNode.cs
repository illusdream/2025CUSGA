using System;

public interface IProcedureNode
{
    public ProcedureSwitcher switcher { get; set; }
    
    public bool IsExecuting { get; set; }
    public void OnInit();
    public void OnEnter();
    public void OnUpdate();
    
    public void OnLateUpdate();
    public void OnFixedUpdate();

    public void OnExit();


    public void OnDestroy();


    public void ChangeState<T>() where T : IProcedureNode;
    
    
    public void ChangeStateByPopStack();

}