using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;

public class BuffManager : ManagerSingleton<BuffManager>, IManager,IAssemblyForeach
{
    private BuffConfig buffconfig;

    private Dictionary<Type, BaseBuffConfig> buffConfigs;

    private BiMap<EBuffType, Type> buffEnum_TypeMap;
    
    [ShowInInspector]
    private Dictionary<EBuffType,EBuffTag> buffType_TagMap;
    
    [ShowInInspector]
    private Dictionary<EBuffTag,List<EBuffType>> buffTag_TypeMap;
    public void Init()
    {
        buffconfig = Config.GetConfig<BuffConfig>();
        
        buffConfigs = new Dictionary<Type, BaseBuffConfig>();
        buffEnum_TypeMap = new BiMap<EBuffType, Type>();

        buffType_TagMap = new Dictionary<EBuffType, EBuffTag>();
        buffTag_TypeMap = new Dictionary<EBuffTag,List<EBuffType>>();

    }
    public void ForeachCurrentAssembly(Type[] types)
    {
        List<EBuffTag> allTags = Enum.GetValues(typeof(EBuffTag)).OfType<EBuffTag>().ToList();
        
        
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
                    
                    var testInstance = Activator.CreateInstance(type) as BaseBuff;
                    buffType_TagMap.Add((EBuffType)buffID,testInstance.BuffTag);

                    foreach (var eBuffTag in allTags)
                    {
                        if (testInstance.BuffTag.HasFlag(eBuffTag))
                        {
                            if (buffTag_TypeMap.TryGetValue(eBuffTag, out List<EBuffType> list))
                            {
                                list.Add((EBuffType)buffID);
                            }
                            else
                            {
                                buffTag_TypeMap[eBuffTag] = new List<EBuffType>(){(EBuffType)buffID};
                            }
                        }
                    }
                    
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

    public bool CheckBuffHasTag(EBuffType type, EBuffTag tag)
    {
        return buffType_TagMap.TryGetValue(type, out EBuffTag _tag) && (tag & _tag) != EBuffTag.None;
    }

    public List<EBuffType> GetAllBuffTypeBySameTag(EBuffTag tag)
    {
        return buffTag_TypeMap.GetValueOrDefault(tag);
    }
}