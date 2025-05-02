using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[AutoBuildOrLoadConfig("VisualEffectConfig")]
public class VisualEffectConfig : ConfigScriptObject
{
    public override string ConfigName => "VisualEffect";
    
    public const string BuffTypeEnumName = "EBuffType";
    
    public const string BuffTypeEnumDescription = "";
    
    public const string VisualEffecctPoolConfigPath = "Assets/Resources/Base/VisualEffect/VisualEffecctPoolConfig";

    [SerializeField]
    [ShowInInspector]
    [LabelText("PropConfigs")]
    [ListDrawerSettings(ShowFoldout = false,HideAddButton = true,HideRemoveButton = true,DraggableItems = false)]
    [Searchable]
    [InlineProperty]
    [ShowInInlineEditors]
    [FoldoutGroup("PropDetailConfig")]
    public List<BaseVisualEffectConfig> VisualEffectPoolConfigs;

    private Dictionary<string, BaseVisualEffectConfig> VisualEffectPoolConfigsDictionary;

    
    [ToggleLeft]
    public bool AutoBuildOrUpdateSingleBuffConfigs = true;
    
    
    #if UNITY_EDITOR
    
    [Button]
    [FoldoutGroup("PropDetailConfig")]
    public void RefreshPropConfigs()
    {
        VisualEffectPoolConfigs = new List<BaseVisualEffectConfig>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseVisualEffectPool>())
        {
            if (type.IsAbstract || type.IsGenericType)
            {
                continue;
            }
            if (Activator.CreateInstance(type) is BaseBuff instance)
            {
                CreateVisualEffectPoolConfigAsset(instance.ConfigType, type,out var buffConfig);
                VisualEffectPoolConfigs.Add(buffConfig);
            }
            
        }
    }
    
    public void CreateVisualEffectPoolConfigAsset(Type propConfigType,Type propType,out BaseVisualEffectConfig property)
    {
        var instance = ScriptableObject.CreateInstance(propConfigType) as BaseVisualEffectConfig;
        if (instance == null)
        {
            property = null;
            throw new ArgumentException($"Buff类{propType.FullName}未定义对应的配置类，请代码继承{typeof(BaseVisualEffectConfig).FullName}");
        }
        instance.TargetType = propType.FullName;
        AssetDatabase.CreateAsset(instance,VisualEffecctPoolConfigPath + $"/{propConfigType.Name}.asset");
        property = instance;
    }
    
    
    public void CheckVisualEffectProperty(List<Type> allPropTypes)
    {
        CheckVisualEffectConfigsDictionaryValid();
        foreach (var type in allPropTypes)
        {
            if (!VisualEffectPoolConfigsDictionary.ContainsKey(type.FullName))
            {
                if (type.IsAbstract || type.IsGenericType)
                {
                    continue;
                }
                if (Activator.CreateInstance(type) is BaseBuff instance)
                {
                    
                    CreateVisualEffectPoolConfigAsset(instance.ConfigType, type,out var PropConfig);
                    VisualEffectPoolConfigs.Add(PropConfig);
                    VisualEffectPoolConfigsDictionary.Add(type.FullName,PropConfig);
                }
            }
        }
    }
    

#endif
    
    
    private void CheckVisualEffectConfigsDictionaryValid()
    {
        VisualEffectPoolConfigsDictionary ??= new Dictionary<string,BaseVisualEffectConfig>();
        foreach (var config in VisualEffectPoolConfigs)
        {
            VisualEffectPoolConfigsDictionary.TryAdd(config.TargetType, config);
        }
    }
    
    public bool TryGetVisualEffectConfig(string typeFullName, out BaseVisualEffectConfig propConfig)
    {
        CheckVisualEffectConfigsDictionaryValid();
        return VisualEffectPoolConfigsDictionary.TryGetValue(typeFullName, out propConfig);
    }
    
}