using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[AutoBuildOrLoadConfig("PropConfig")]
public class PropConfig : ConfigScriptObject
{
    public const string PropTypeEnumName = "EPropType";
    public const string PropTypeEnumDescription = "";
    
    public const string PropConfigsFolderPath = "Assets/Resources/Base/Prop/PropConfigs";
    public override string ConfigName => "PropConfig";
    [SerializeField]
    [ShowInInspector]
    [LabelText("PropConfigs")]
    [ListDrawerSettings(ShowFoldout = false,HideAddButton = true,HideRemoveButton = true,DraggableItems = false)]
    [Searchable]
    [InlineProperty]
    [ShowInInlineEditors]
    [FoldoutGroup("PropDetailConfig")]
    public List<BasePropConfig> PropConfigs;

    private Dictionary<string,BasePropConfig> PropConfigsDictionary;

    public SerializableDictionary<string, int> PropIDsMap;
    
    [ValueDropdown("GetAllPropTypes",IsUniqueList = true)]
    [ListDrawerSettings(DraggableItems = false)]
    public List<EPropType> BeRandomSelectProps;

    public List<EPropType> GetAllPropTypes()
    {
        return new List<EPropType>(Enum.GetValues(typeof(EPropType)).OfType<EPropType>());
    }
    
    public bool AutoBuildOrUpdateSinglePropConfigs = true;





#if UNITY_EDITOR
    [Button]
    [FoldoutGroup("PropDetailConfig")]
    public void RefreshPropConfigs()
    {
        PropConfigs = new List<BasePropConfig>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseProp>())
        {
            if (Activator.CreateInstance(type) is BaseProp instance)
            {
                CreatePropConfigAsset(instance.ConfigType, type,out var PropConfig);
                PropConfigs.Add(PropConfig);
            }
            
        }
    }
    public void CreatePropConfigAsset(Type propConfigType,Type propType,out BasePropConfig property)
    {
        var instance = ScriptableObject.CreateInstance(propConfigType) as BasePropConfig;
        if (instance == null)
        {
            property = null;
            throw new ArgumentException($"道具类{propType.FullName}未定义对应的配置类，请代码继承{typeof(BasePropConfig).FullName}");
        }
        instance.TargetType = propType.FullName;
        AssetDatabase.CreateAsset(instance,PropConfigsFolderPath + $"/{propConfigType.Name}.asset");
        property = instance;
    }

    public void CheckTileProperty(List<Type> allPropTypes)
    {
        CheckPropConfigsDictionaryValid();
        foreach (var type in allPropTypes)
        {
            if (!PropConfigsDictionary.ContainsKey(type.FullName))
            {
                if (Activator.CreateInstance(type) is BaseProp instance)
                {
                    CreatePropConfigAsset(instance.ConfigType, type,out var PropConfig);
                    PropConfigs.Add(PropConfig);
                    PropConfigsDictionary.Add(type.FullName,PropConfig);
                }
            }
        }
    }
    [Button]
    public void ReBuildPropIDMap()
    {
        int IDCounter = 0;
        PropIDsMap = new SerializableDictionary<string, int>();
        foreach (var type in TypeCache.GetTypesDerivedFrom<BaseProp>())
        {
            PropIDsMap.TryAdd(type.Name, IDCounter);
            IDCounter++;
        }
    }
    [Button]
    public void ReBuildPropIDEnum()
    {
        if (PropIDsMap == null || PropIDsMap.Count == 0)
        {
            return;
        }
        ScriptGenerator generator = new ScriptGenerator();
        EnumGenerator enumGenerator = new EnumGenerator(EAccessType.Public, PropTypeEnumName, PropTypeEnumDescription);
        foreach (var NameID_KVP in PropIDsMap)
        {
            enumGenerator.Append((NameID_KVP.Key,NameID_KVP.Value));
        }
        generator.Append(enumGenerator);
        
        StackTrace st  = new StackTrace(0,true);
        
        DirectoryInfo directoryInfo = new DirectoryInfo(st.GetFrame(0).GetFileName());

        string parentPath = directoryInfo.Parent.FullName;
        generator.GenerateScript(PropTypeEnumName,parentPath);
        AssetDatabase.Refresh();
    }
#endif

    private void CheckPropConfigsDictionaryValid()
    {
        PropConfigsDictionary ??= new Dictionary<string,BasePropConfig>();
        foreach (var config in PropConfigs)
        {
            PropConfigsDictionary.TryAdd(config.TargetType, config);
        }
    }

    public bool TryGetPropConfig(string typeFullName, out BasePropConfig propConfig)
    {
        CheckPropConfigsDictionaryValid();
        return PropConfigsDictionary.TryGetValue(typeFullName, out propConfig);
    }

    public bool TryGetPropID(string typeName, out int propID)
    {
        return PropIDsMap.TryGetValue(typeName, out propID);
    }
    
}