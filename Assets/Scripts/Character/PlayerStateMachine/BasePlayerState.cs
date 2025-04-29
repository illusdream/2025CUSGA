using System;
using ilsFramework;

public interface IPlayerState : IState
{
    public EntityHandler EntityHandler { get;}
    
    public PlayerController PlayerController { get;}
}

public class BasePlayerState : IPlayerState
{
    private EntityHandler entityHandler;
    
    public EntityHandler EntityHandler => entityHandler;
    
    private PlayerController playerController;
    
    public PlayerController PlayerController => playerController;

    public PlayerStateMachine fsm { get; set; }
    
    public BasePlayerState(EntityHandler handler,PlayerController playerController)
    {
        entityHandler = handler;
        this.playerController = playerController;
    }
    public virtual void OnInit()
    {
        
    }

    public virtual  void OnEnter()
    {
        
    }

    public virtual  void OnUpdate()
    {
       
    }

    public virtual  void OnFixedUpdate()
    {
      
    }

    public virtual  void OnExit()
    {
        
    }

    public virtual  void OnDestroy()
    {
        
    }

    public void ChangeState<T>() where T : BasePlayerState
    {
        fsm.ChangeState<T>();
    }

    public void ChangeState(Type stateType)
    {
        fsm.ChangeState(stateType);
    }
}