using System;
using System.Collections.Generic;
using ilsFramework;

public class BuffManager : ManagerSingleton<BuffManager>, IManager,IAssemblyForeach
{
    private BuffConfig buffconfig;

    private Dictionary<Type, BaseBuffConfig> buffConfigs;

    private BiMap<EBuffType, Type> buffEnum_TypeMap;
    
    
    public void Init()
    {
        buffconfig = Config.GetConfig<BuffConfig>();
        
        buffConfigs = new Dictionary<Type, BaseBuffConfig>();
        buffEnum_TypeMap = new BiMap<EBuffType, Type>();
        
        
        
    }
    public void ForeachCurrentAssembly(Type[] types)
    {
        foreach (var type in types)
        {
            if (typeof(BaseBuff).IsAssignableFrom(type) && !type.IsAbstract)
            {
                if (buffconfig.TryGetPropConfig(type.FullName,out var baseBuffConfig))
                {
                    buffConfigs.Add(type, baseBuffConfig);
                }

                if (buffconfig.TryGetPropID(type.Name,out var buffID))
                {
                    buffEnum_TypeMap.Add((EBuffType)buffID, type);
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


    public bool TryGetBuffConfig<T>(Type type, out T config) where T : BaseBuffConfig
    {
        if (buffConfigs.TryGetValue(type, out BaseBuffConfig baseConfig) && baseConfig is T _config)
        {
            config = _config;
            return true;
        }
        config = null;
        return false;
    }

    public BaseBuff CreateInstance(EBuffType type)
    {
        if (buffEnum_TypeMap.TryGetRight(type,out var classType))
        {
            BaseBuff buff = Activator.CreateInstance(classType) as BaseBuff;
            return buff;
        }

        return null;
    }


}