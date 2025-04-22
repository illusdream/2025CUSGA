using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[AutoBuildOrLoadConfig("BuffConfig")]
public class BuffConfig : ConfigScriptObject
{
    public override string ConfigName => "BuffConfig";
    
    public const string BuffTypeEnumName = "EBuffType";
    
    public const string BuffTypeEnumDescription = "";
    
    public const string BuffConfigsFolderPath = "Assets/Resources/Base/Buff/BuffConfigs";

    [SerializeField]
    [ShowInInspector]
    [LabelText("PropConfigs")]
    [ListDrawerSettings(ShowFoldout = false,HideAddButton = true,HideRemoveButton = true,DraggableItems = false)]
    [Searchable]
    [InlineProperty]
    [ShowInInlineEditors]
    [FoldoutGroup("PropDetailConfig")]
    public List<BaseBuffConfig> BuffConfigs;

    private Dictionary<string, BaseBuffConfig> BuffConfigsDictionary;

    public SerializableDictionary<string, int> BuffIDsMap;
    
    [ToggleLeft]
    public bool AutoBuildOrUpdateSingleBuffConfigs = true;



#if UNITY_EDITOR
    
    [Button]
    [FoldoutGroup("PropDetailConfig")]
    public void RefreshPropConfigs()
    {
        BuffConfigs = new List<BaseBuffConfig>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseBuff>())
        {
            if (type.IsAbstract || type.IsGenericType)
            {
                continue;
            }
            if (Activator.CreateInstance(type) is BaseBuff instance)
            {
                CreateBuffConfigAsset(instance.ConfigType, type,out var buffConfig);
                BuffConfigs.Add(buffConfig);
            }
            
        }
    }
    
    public void CreateBuffConfigAsset(Type propConfigType,Type propType,out BaseBuffConfig property)
    {
        var instance = ScriptableObject.CreateInstance(propConfigType) as BaseBuffConfig;
        if (instance == null)
        {
            property = null;
            throw new ArgumentException($"Buff类{propType.FullName}未定义对应的配置类，请代码继承{typeof(BaseBuffConfig).FullName}");
        }
        instance.TargetType = propType.FullName;
        AssetDatabase.CreateAsset(instance,BuffConfigsFolderPath + $"/{propConfigType.Name}.asset");
        property = instance;
    }
    
    
    public void CheckBuffProperty(List<Type> allPropTypes)
    {
        CheckPropConfigsDictionaryValid();
        foreach (var type in allPropTypes)
        {
            if (!BuffConfigsDictionary.ContainsKey(type.FullName))
            {
                if (type.IsAbstract || type.IsGenericType)
                {
                    continue;
                }
                if (Activator.CreateInstance(type) is BaseBuff instance)
                {
                    
                    CreateBuffConfigAsset(instance.ConfigType, type,out var PropConfig);
                    BuffConfigs.Add(PropConfig);
                    BuffConfigsDictionary.Add(type.FullName,PropConfig);
                }
            }
        }
    }
    
    [Button]
    public void ReBuildBuffIDMap()
    {
        int IDCounter = 0;
        BuffIDsMap = new SerializableDictionary<string, int>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseBuff>())
        {
            if (!type.IsAbstract && !type.IsGenericType)
            {
                BuffIDsMap.TryAdd(type.Name, IDCounter);
                IDCounter++;
            }
        }
    }
    
    [Button]
    public void ReBuildPropIDEnum()
    {
        if (BuffIDsMap == null || BuffIDsMap.Count == 0)
        {
            return;
        }
        ScriptGenerator generator = new ScriptGenerator();
        EnumGenerator enumGenerator = new EnumGenerator(EAccessType.Public, BuffTypeEnumName, BuffTypeEnumDescription);
        foreach (var NameID_KVP in BuffIDsMap)
        {
            enumGenerator.Append((NameID_KVP.Key,NameID_KVP.Value));
        }
        generator.Append(enumGenerator);
        
        StackTrace st  = new StackTrace(0,true);
        
        DirectoryInfo directoryInfo = new DirectoryInfo(st.GetFrame(0).GetFileName());

        string parentPath = directoryInfo.Parent.FullName;
        generator.GenerateScript(BuffTypeEnumName,parentPath);
        AssetDatabase.Refresh();
    }
#endif
    
    
    private void CheckPropConfigsDictionaryValid()
    {
        BuffConfigsDictionary ??= new Dictionary<string,BaseBuffConfig>();
        foreach (var config in BuffConfigs)
        {
            BuffConfigsDictionary.TryAdd(config.TargetType, config);
        }
    }
    
    public bool TryGetPropConfig(string typeFullName, out BaseBuffConfig propConfig)
    {
        CheckPropConfigsDictionaryValid();
        return BuffConfigsDictionary.TryGetValue(typeFullName, out propConfig);
    }

    public bool TryGetPropID(string typeName, out int propID)
    {
        return BuffIDsMap.TryGetValue(typeName, out propID);
    }
}