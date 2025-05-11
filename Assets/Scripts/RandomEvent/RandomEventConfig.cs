using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[AutoBuildOrLoadConfig("RandomEventConfig")]
public class RandomEventConfig : ConfigScriptObject
{
    public override string ConfigName => "RandomEvent";
    
    
    public const string RandomEventTypeEnumName = "ERandomEventType";
    public const string RandomEventEnumDescription = "";
    
    public const string RandomEventConfigPath = "Assets/Resources/Base/RandomEvent/RandomEventConfig";

    [FormerlySerializedAs("VisualEffectPoolConfigs")]
    [SerializeField]
    [ShowInInspector]
    [LabelText("PropConfigs")]
    [ListDrawerSettings(ShowFoldout = false,HideAddButton = true,HideRemoveButton = true,DraggableItems = false)]
    [Searchable]
    [InlineProperty]
    [ShowInInlineEditors]
    [FoldoutGroup("PropDetailConfig")]
    public List<BaseRandomEventConfig> RandomEventConfigs;

    private Dictionary<string, BaseRandomEventConfig> RandomEventConfigsDictionary;

    
    public SerializableDictionary<string, int> RandomEventIDsMap;
    
    [ToggleLeft]
    public bool AutoBuildOrUpdateSingleRandomEventConfigs = true;

    public int RandomEventInterval;
    
    [FormerlySerializedAs("BeRandomSelectProps")]
    [ValueDropdown("GetAllPropTypes")]
    [ListDrawerSettings(DraggableItems = false)]
    public List<ERandomEventType> BeRandomSelectRandomEvent;

    public List<ERandomEventType> GetAllPropTypes()
    {
        return new List<ERandomEventType>(Enum.GetValues(typeof(ERandomEventType)).OfType<ERandomEventType>());
    }
    #if UNITY_EDITOR
    
    [Button]
    [FoldoutGroup("PropDetailConfig")]
    public void RefreshRandomEventConfigs()
    {
        RandomEventConfigs = new List<BaseRandomEventConfig>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseRandomEvent>())
        {
            if (type.IsAbstract || type.IsGenericType)
            {
                continue;
            }
            if (Activator.CreateInstance(type) is BaseRandomEvent instance)
            {
                CreateVisualEffectPoolConfigAsset(instance.ConfigType, type,out var buffConfig);
                RandomEventConfigs.Add(buffConfig);
            }
            
        }
    }
    
    public void CreateVisualEffectPoolConfigAsset(Type propConfigType,Type propType,out BaseRandomEventConfig property)
    {
        var instance = ScriptableObject.CreateInstance(propConfigType) as BaseRandomEventConfig;
        if (instance == null)
        {
            property = null;
            throw new ArgumentException($"Buff类{propType.FullName}未定义对应的配置类，请代码继承{typeof(BaseRandomEventConfig).FullName}");
        }
        instance.TargetType = propType.FullName;
        AssetDatabase.CreateAsset(instance,RandomEventConfigPath + $"/{propConfigType.Name}.asset");
        property = instance;
    }
    
    
    public void CheckVisualEffectProperty(List<Type> allPropTypes)
    {
        CheckVisualEffectConfigsDictionaryValid();
        foreach (var type in allPropTypes)
        {
            if (!RandomEventConfigsDictionary.ContainsKey(type.FullName))
            {
                if (type.IsAbstract || type.IsGenericType)
                {
                    continue;
                }
                if (Activator.CreateInstance(type) is BaseRandomEvent instance)
                {
                    CreateVisualEffectPoolConfigAsset(instance.ConfigType, type,out var PropConfig);
                    RandomEventConfigs.Add(PropConfig);
                    RandomEventConfigsDictionary.Add(type.FullName,PropConfig);
                }
            }
        }
    }
    
    [Button]
    public void ReBuildPropIDMap()
    {
        int IDCounter = 0;
        RandomEventIDsMap = new SerializableDictionary<string, int>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseRandomEvent>())
        {
            if (type.IsAbstract || type.IsGenericType)
            {
                continue;
            }
            RandomEventIDsMap.TryAdd(type.Name, IDCounter);
            IDCounter++;
        }
    }
    [Button]
    public void ReBuildPropIDEnum()
    {
        if (RandomEventIDsMap == null || RandomEventIDsMap.Count == 0)
        {
            return;
        }
        ScriptGenerator generator = new ScriptGenerator();
        EnumGenerator enumGenerator = new EnumGenerator(EAccessType.Public, RandomEventTypeEnumName, RandomEventEnumDescription);
        foreach (var NameID_KVP in RandomEventIDsMap)
        {
            enumGenerator.Append((NameID_KVP.Key,NameID_KVP.Value));
        }
        generator.Append(enumGenerator);
        
        StackTrace st  = new StackTrace(0,true);
        
        DirectoryInfo directoryInfo = new DirectoryInfo(st.GetFrame(0).GetFileName());

        string parentPath = directoryInfo.Parent.FullName;
        generator.GenerateScript(RandomEventTypeEnumName,parentPath);
        AssetDatabase.Refresh();
    }
#endif
    
    
    private void CheckVisualEffectConfigsDictionaryValid()
    {
        RandomEventConfigsDictionary ??= new Dictionary<string,BaseRandomEventConfig>();
        foreach (var config in RandomEventConfigs)
        {
            RandomEventConfigsDictionary.TryAdd(config.TargetType, config);
        }
    }
    
    public bool TryGetVisualEffectConfig(string typeFullName, out BaseRandomEventConfig propConfig)
    {
        CheckVisualEffectConfigsDictionaryValid();
        return RandomEventConfigsDictionary.TryGetValue(typeFullName, out propConfig);
    }
    
    public bool TryGetPropID(string typeName, out int propID)
    {
        return RandomEventIDsMap.TryGetValue(typeName, out propID);
    }

}