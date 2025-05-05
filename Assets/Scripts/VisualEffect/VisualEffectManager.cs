using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class VisualEffectManager : ManagerSingleton<VisualEffectManager>,IManager,IAssemblyForeach
{
    private VisualEffectConfig visualEffectConfig;

    private Dictionary<Type, BaseVisualEffectConfig> visualEffectConfigs;
    [ShowInInspector]
    private Dictionary<Type,BaseVisualEffectPool> visualEffectPools;
    public void Init()
    {
        visualEffectConfig = Config.GetConfig<VisualEffectConfig>();
        
        visualEffectConfigs = new Dictionary<Type, BaseVisualEffectConfig>();
        
        visualEffectPools = new Dictionary<Type, BaseVisualEffectPool>();
    }
    
    public void ForeachCurrentAssembly(Type[] types)
    {
        foreach (var type in types)
        {
            if (typeof(BaseVisualEffectPool).IsAssignableFrom(type) && !type.IsAbstract)
            {
                if (visualEffectConfig.TryGetVisualEffectConfig(type.FullName,out var baseBuffConfig))
                {
                    visualEffectConfigs.Add(type, baseBuffConfig);
                    
                    
                    var instance = Activator.CreateInstance(type) as BaseVisualEffectPool;
                    
                    instance._config = baseBuffConfig;
                    var obj = new GameObject(type.FullName);
                    obj.transform.parent = ContainerObject.transform;
                    instance.VisualPoolContainer = obj;
                    instance.InitPool();

                    visualEffectPools.Add(type,instance);
                } 
            }
        }
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

    public bool TryGetVisualEffectPool<T>(out T result) where T : BaseVisualEffectPool
    {
        if (visualEffectPools.TryGetValue(typeof(T),out var _result) && _result is T final)
        {
            result = final;
            return true;
        }
        result =null;
        return false;

        ;
    }

    public void ClearAllVisualEffectPools()
    {
        foreach (var pool in visualEffectPools.Values)
        {
            pool.Clear();
        }
    }

}