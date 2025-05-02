using System;
using System.Collections.Generic;
using ilsFramework;

public class VisualEffectManager : ManagerSingleton<VisualEffectManager>,IManager,IAssemblyForeach
{
    private VisualEffectConfig visualEffectConfig;

    private Dictionary<Type, BaseVisualEffectConfig> visualEffectConfigs;
    
    private Dictionary<Type,BaseVisualEffectPool> visualEffectPools;
    public void Init()
    {
        visualEffectConfig = Config.GetConfig<VisualEffectConfig>();
        
        visualEffectConfigs = new Dictionary<Type, BaseVisualEffectConfig>();
        
        visualEffectPools = new Dictionary<Type, BaseVisualEffectPool>();
    }
    
    public void ForeachCurrentAssembly(Type[] types)
    {
        
    }

    public void Update()
    {
        
    }

    public void LateUpdate()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void OnDestroy()
    {
        
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        
    }


}