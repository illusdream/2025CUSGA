using System;
using ilsFramework;

public abstract class BaseRandomEvent
{

    public abstract Type ConfigType { get; }
        
    public BaseRandomEventConfig _config;

    public string ID;
    
    public bool IsValid => !EventTimer.IsFinish;
    
    public Timer EventTimer;

    public void Init()
    {
        EventTimer =  (new TimerBuilder(_config.EventLastTime, 1)).Register();
        OnInit();
    }
    
    public abstract void OnInit();

    public abstract void OnEventStart();

    public abstract void OnEventUpdate();
    
    public abstract void OnEventFixedUpdate();
    
    public abstract void OnEventEnd();
    
    public abstract void OnEventDestroy();
    
}

public abstract class BaseRandomEvent<T> : BaseRandomEvent where T : BaseRandomEventConfig
{
    public override Type ConfigType => typeof(T);
    
    public T Config => (T)_config;
}