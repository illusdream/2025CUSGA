using System;
using ilsFramework;

public class SubProcedureSwitcher : ProcedureSwitcher,IProcedureNode
{
    public ProcedureSwitcher switcher { get; set; }
    public bool IsExecuting { get; set; }

    public virtual void OnInit()
    {
        
    }

    public virtual void OnEnter()
    {
        _currentState.IsExecuting = true;
        _currentState.OnEnter();
    }

    public virtual void OnFixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public virtual void OnExit()
    {
        _currentState.IsExecuting = false;
        _currentState.OnExit();
    }

    public virtual void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public virtual void OnLateUpdate()
    {
       _currentState.OnLateUpdate();
    }

    public virtual void ChangeState<T>() where T : IProcedureNode
    {
        _currentState.OnExit();
        switcher.ChangeProcedureNode<T>();
    }

    public virtual void ChangeStateByPopStack()
    {
        ChangeProcedureByPopStack();
    }
    

    public virtual void SelfChangeProcedureByPopStack()
    {
        switcher.ChangeProcedureByPopStack();
    }

}