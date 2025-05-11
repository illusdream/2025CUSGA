using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Props;
using Sirenix.OdinInspector;

public class PropManager : ManagerSingleton<PropManager>,IManager,IAssemblyForeach
{
    private PropConfig propsConfig;
    
    private Dictionary<Type, BasePropConfig> propConfigs;
    private BiMap<int, Type> propID_TypeMap;
    [ShowInInspector]
    private BiMap<EPropType, Type> propEnum_TypeMap;

    public List<EPropType> CurrentRandomSelectList;
    public void Init()
    {
        propsConfig = Config.GetConfig<PropConfig>();

        CurrentRandomSelectList = propsConfig.BeRandomSelectProps;
        
        propConfigs = new Dictionary<Type, BasePropConfig>();
        propID_TypeMap = new BiMap<int, Type>();
        propEnum_TypeMap = new BiMap<EPropType, Type>();
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
                    propEnum_TypeMap.Add((EPropType)propID, type);
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
    
    public bool TryGetPropConfig<T>(EPropType type, out T propConfig) where T : BasePropConfig
    {
        if (propEnum_TypeMap.TryGetRight(type,out var classType) &&propConfigs.TryGetValue(classType, out var basePropConfig) && basePropConfig is T _propConfig)
        {
            propConfig = _propConfig;
            return true;
        }

        propConfig = null;
        return false;
    }

    public List<EPropType> GetAllEPropTypes()
    {
        return propsConfig.AllCanBeSelectProps.ToList();
    }
    

    public BaseProp CreateTargetProp(Type type)
    {
        _ = "11";
        BaseProp baseProp = Activator.CreateInstance(type) as BaseProp;
        if (baseProp != null && TryGetPropConfig<BasePropConfig>(type, out var propConfig))
        {
            baseProp.Initialize(propConfig);
            return baseProp;
        }

        return null;
    }

    public BaseProp CreateTargetProp(EPropType propType)
    {
        return CreateTargetProp(propEnum_TypeMap.GetRight(propType));
    }
    
    public BaseProp CreateRandomProp()
    {
        var randomResult = CurrentRandomSelectList.Shuffle().FirstOrDefault();
        return CreateTargetProp(propEnum_TypeMap.GetRight(randomResult));
    }

    public List<EPropType> GetDefaultBeSelectRandomPropList()
    {
        return propsConfig.BeRandomSelectProps.ToList();
    }

    public void SetRandomSelectList(List<EPropType> types)
    {
        CurrentRandomSelectList = types;
    }

    public void SetDefaultRandomSelectList()
    {
        CurrentRandomSelectList = propsConfig.BeRandomSelectProps;
    }
}