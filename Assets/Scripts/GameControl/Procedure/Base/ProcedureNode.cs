using System;
using ilsFramework;

public abstract class ProcedureNode : IProcedureNode
{
    public ProcedureSwitcher switcher { get; set; }
    public bool IsExecuting { get; set; }

    public virtual void OnInit()
    {
        
    }

    public virtual void OnEnter()
    {
        
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnLateUpdate()
    {
        
    }

    public virtual void OnFixedUpdate()
    {
        
    }

    public virtual void OnExit()
    {
        
    }

    public virtual void OnDestroy()
    {
        
    }

    public virtual void ChangeState<T>() where T : IProcedureNode
    {
        switcher?.ChangeProcedureNode<T>();
    }
    

    public virtual void ChangeStateByPopStack()
    {
        switcher?.ChangeProcedureByPopStack();    
    }
}