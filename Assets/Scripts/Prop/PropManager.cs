using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Props;

public class PropManager : ManagerSingleton<PropManager>,IManager,IAssemblyForeach
{
    private PropConfig propsConfig;
    
    private Dictionary<Type, BasePropConfig> propConfigs;
    private BiMap<int, Type> propID_TypeMap;
    public void Init()
    {
        propsConfig = Config.GetConfig<PropConfig>();
        
        propConfigs = new Dictionary<Type, BasePropConfig>();
        propID_TypeMap = new BiMap<int, Type>();
    }
    public void ForeachCurrentAssembly(Type[] types)
    {
        foreach (var type in types)
        {
            if (typeof(BaseProp).IsAssignableFrom(type) && !type.IsAbstract )
            {
                if ( propsConfig.TryGetPropConfig(type.FullName,out var basePropConfig))
                {
                    propConfigs.Add(type, basePropConfig);
                }

                if (propsConfig.TryGetPropID(type.Name, out var propID))
                {
                    propID_TypeMap.Add(propID, type);
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
    
    //查询
    public bool TryGetPropConfig<T>(Type type, out T propConfig) where T : BasePropConfig
    {
        if (propConfigs.TryGetValue(type, out var basePropConfig) && basePropConfig is T _propConfig)
        {
            propConfig = _propConfig;
            return true;
        }

        propConfig = null;
        return false;
    }

    public BaseProp CreateTargetProp(Type type)
    {
        BaseProp baseProp = Activator.CreateInstance(type) as BaseProp;
        if (baseProp != null && TryGetPropConfig<BasePropConfig>(type, out var propConfig))
        {
            baseProp.Initialize(propConfig);
            return baseProp;
        }

        return null;
    }
    
    public BaseProp CreateRandomProp()
    {
        var selectList = propID_TypeMap.Select((p => p.Value)).ToList();
        var randomResult = selectList.Shuffle()[0];
        return CreateTargetProp(randomResult);
    }

}